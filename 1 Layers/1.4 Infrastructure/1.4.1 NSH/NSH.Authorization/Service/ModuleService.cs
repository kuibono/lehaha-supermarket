using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Transaction.Interceptor;
using NSH.Authorization.Domain;
using NSH.Core.Domain;
using NHibernate.Linq;
using System.Linq.Expressions;

namespace NSH.Authorization.Service
{
    public class ModuleService : IModuleService
    {
        public IRepository<Module> ModuleRepository { get; set; }

        #region IModuleService Members

        [Transaction]
        public int Create(Module entity)
        {
            return ModuleRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public Module GetById(int id)
        {
            return ModuleRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public Module GetByName(string name)
        {
            return ModuleRepository.LinqQuery.Where(o => o.ModuleName == name).FirstOrDefault();
        }

        [Transaction(ReadOnly = true)]
        public IList<Module> GetAll()
        {
            return ModuleRepository.LinqQuery.Cacheable().ToList();
        }

        [Transaction]
        public void Update(Module entity)
        {
            ModuleRepository.Update(entity);
        }

        [Transaction]
        public void WhereUpdate(Action<Module> updater,
        Expression<Func<Module, bool>> predicate = null)
        {
            var query = ModuleRepository.LinqQuery;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (var entity in query)
            {
                updater(entity);
                ModuleRepository.Update(entity);
            }
        }

        [Transaction]
        public void Delete(IList<Module> entitys)
        {
            foreach (Module entity in entitys)
            {
                ModuleRepository.Delete(entity);
            }
        }
        #endregion
    }
}
