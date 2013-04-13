using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Authorization.Domain;

namespace NSH.Authorization.Service
{
    public class RoleService : IRoleService
    {
        public IRepository<Role> RoleRepository { get; set; }

        #region IRoleService Members

        [Transaction]
        public int Create(Role entity)
        {
            return RoleRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public Role GetById(int id)
        {
            return RoleRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<Role> GetAll()
        {
            return RoleRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(Role entity)
        {
            RoleRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Role entity)
        {
            RoleRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Role> entitys)
        {
            foreach (Role entity in entitys)
            {
                RoleRepository.Delete(entity);
            }
        }

        #endregion
    }
}
