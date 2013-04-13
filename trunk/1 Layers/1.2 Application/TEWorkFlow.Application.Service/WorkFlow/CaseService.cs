using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using TEWorkFlow.Domain.WorkFlow;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class CaseService : ICaseService
    {
        public IRepository<Case> CaseRepository { get; set; }

        [Transaction]
        public int Create(Case entity)
        {
            return CaseRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public Case GetById(int id)
        {
            return CaseRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<Case> GetAll()
        {
            return CaseRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(Case entity)
        {
            CaseRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Case entity)
        {
            CaseRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Case> entitys)
        {
            foreach (Case entity in entitys)
            {
                CaseRepository.Delete(entity);
            }
        }

    }
}
