using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Transaction.Interceptor;
using NSH.Authorization.Domain;
using NSH.Core.Domain;
using NHibernate.Linq;

namespace NSH.Authorization.Service
{
    public class GroupService : IGroupService
    {
        public IRepository<Group> GroupRepository { get; set; }

        [Transaction]
        public int Create(Group entity)
        {
            return GroupRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public Group GetById(int id)
        {
            return GroupRepository.LinqQuery
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        [Transaction(ReadOnly = true)]
        public Group GetRoot()
        {
            return GroupRepository
                .LinqQuery.Where(o => o.IsRoot)
                .FirstOrDefault();
        }

        [Transaction(ReadOnly = true)]
        public IList<Group> GetAll()
        {
            return GroupRepository.LinqQuery.Cacheable().ToList();
        }

        [Transaction]
        public void Update(Group entity)
        {
            GroupRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Group entity)
        {
            GroupRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Group> entitys)
        {
            foreach (Group entity in entitys)
            {
                GroupRepository.Delete(entity);
            }
        }
    }
}
