using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Authorization.Domain;
using System.Linq.Expressions;
using NSH.Core.DataPage;

namespace NSH.Authorization.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 保存指定用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(User entity);

        /// <summary>
        /// 更新指定用户
        /// </summary>
        /// <param name="entity"></param>
        void Update(User entity);

        /// <summary>
        /// 删除指定列表中的用户
        /// </summary>
        /// <param name="selectedItems"></param>
        void Delete(IList<User> selectedItems);

        /// <summary>
        /// 获得所有用户
        /// </summary>
        /// <returns></returns>
        IList<User> GetUserList();

        /// <summary>
        /// 根据登录名和用户名获得用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        IList<User> GetUserList(string loginName, string userName);

        /// <summary>
        /// 根据登录名和用户名获得用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<User> GetUserList(string loginName, string userName, IDataPage dataPage, IDataOrder dataOrder);

        /// <summary>
        /// 根据指定条件获得用户
        /// </summary>
        /// <returns></returns>
        IList<User> GetUserList(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// 根据多个Id获取用户列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<User> GetUserList(IEnumerable<int> ids);

        /// <summary>
        /// 根据指定Id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);

        /// <summary>
        /// 获取指定用户所在组中包含的所有用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IList<User> GetGroupUserList(User user, bool? isLeader=null);

        /// <summary>
        /// 判断登录用户合法性
        /// </summary>
        /// <param name="CurrentEntity"></param>
        /// <returns></returns>
        bool CheckLoginUser(ref User entity);

        /// <summary>
        /// 重置指定列表中的用户的密码为登录名
        /// </summary>
        /// <param name="selectedItems"></param>
        void ResetPassword(IList<User> SelectedItems);

        /// <summary>
        /// 修改指定用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        void ChangePassword(int id, string password);

        /// <summary>
        /// 从一个用户复制权限到其它用户
        /// </summary>
        /// <param name="srcUser"></param>
        /// <param name="dstUserList"></param>
        void CopyPermission(User srcUser, IList<User> dstUserList);
    }
}
