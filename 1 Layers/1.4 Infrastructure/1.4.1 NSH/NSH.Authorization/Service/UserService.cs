using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;
using Spring.Transaction.Interceptor;
using NSH.Authorization.Domain;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using NSH.Core.DataPage;

namespace NSH.Authorization.Service
{
    public class UserService : IUserService
    {
        public IRepository<User> UserRepository { get; set; }

        #region IUserService Members

        [Transaction]
        public int Create(User entity)
        {
            return UserRepository.Save(entity);
        }

        [Transaction]
        public void Update(User entity)
        {
            UserRepository.Update(entity);
        }

        [Transaction]
        public void Delete(IList<User> selectedItems)
        {
            foreach (User user in selectedItems)
            {
                UserRepository.Delete(user);
            }
        }

        [Transaction]
        public void ResetPassword(IList<User> selectedItems)
        {
            User olduser;
            foreach (User user in selectedItems)
            {
                olduser = UserRepository.Get(user.Id);
                olduser.Password = olduser.LoginName.ToMD5();
                UserRepository.Update(olduser);
            }
        }

        [Transaction]
        public void ChangePassword(int id, string password)
        {
            User user = UserRepository.Get(id);
            if (user != null)
            {
                user.Password = password;
                UserRepository.Update(user);
            }
        }

        [Transaction]
        public void CopyPermission(User srcUser, IList<User> dstUserList)
        {
            srcUser = UserRepository.Get(srcUser.Id);
            var dstIdList = dstUserList.ToList().ConvertAll(o => o.Id);
            dstUserList = UserRepository.LinqQuery.Where(o => dstIdList.Contains(o.Id)).ToList();
            if (srcUser != null)
            {
                foreach (User dstUser in dstUserList)
                {
                    List<Module> moduleList = new List<Module>();
                    moduleList.AddRange(srcUser.ModuleList);
                    dstUser.ModuleList = moduleList;
                    UserRepository.Update(dstUser);
                }
            }
        }

        [Transaction(ReadOnly = true)]
        public User GetById(int id)
        {
            return UserRepository.LinqQuery
                .Where(o => o.Id == id)
                .FetchMany(o => o.ModuleList)
                .FetchMany(o => o.GroupList)
                .FirstOrDefault();
        }

        [Transaction(ReadOnly = true)]
        public IList<User> GetUserList()
        {
            return GetUserList(null,null);
        }

        [Transaction(ReadOnly = true)]
        public IList<User> GetUserList(string loginName, string userName)
        {
            return GetUserList(loginName, userName, null, null);
        }

        [Transaction(ReadOnly = true)]
        public IList<User> GetUserList(string loginName, string userName, IDataPage dataPage, IDataOrder dataOrder)
        {
            var query = UserRepository.LinqQuery;
            if (!String.IsNullOrWhiteSpace(loginName))
            {
                query = query.Where(o => o.LoginName == loginName);
            }
            if (!String.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(o => o.UserName == userName);
            }
            if (dataOrder != null)
            {
                if (String.IsNullOrWhiteSpace(dataOrder.By))
                {
                    dataOrder.By = "Id";
                }
                query = query.OrderBy(dataOrder.By
                    + (String.IsNullOrWhiteSpace(dataOrder.Order) ? "" : " " + dataOrder.Order));
            }
            if (dataPage != null)
            {
                query = query.CountAndPage(dataPage);
            }
            return query.ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<User> GetUserList(Expression<Func<User, bool>> predicate)
        {
            return UserRepository.LinqQuery.Where(predicate).OrderByDescending(o => o.Id).ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<User> GetUserList(IEnumerable<int> ids)
        {
            var idArr = ids.ToArray();
            return UserRepository.LinqQuery.Where(o => idArr.Contains(o.Id)).ToList();
        }



        private IList<User> userList;

        [Transaction(ReadOnly = true)]
        public IList<User> GetGroupUserList(User user, bool? isLeader)
        {
            //if (userList == null)
            //{
            IList<User> userList;
            user = UserRepository.LinqQuery
                .Cacheable()
                .Where(o => o.Id == user.Id)
                .FirstOrDefault();
            userList = new List<User>();
            foreach (var group in user.GroupList)
            {
                foreach (var groutUser in group.UserList)
                {
                    if (!userList.Any(o => o.Id == groutUser.Id))
                    {
                        userList.Add(groutUser);
                    }
                }
            }
            //}
            return userList;
        }

        [Transaction(ReadOnly = true)]
        public bool CheckLoginUser(ref User entity)
        {
            User user = entity;
            user = UserRepository.LinqQuery
                .Where(o => o.LoginName == user.LoginName && o.Password == user.Password)
                //.Fetch(o => o.ModuleList)
                .FirstOrDefault();
            entity = user;
            return user != null;
        }

        #endregion
    }
}
