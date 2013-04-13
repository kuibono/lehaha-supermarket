using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TEWorkFlow.Domain.WorkFlow;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class WorkStepService:IWorkStepService
    {
        public IRepository<WorkStep> WorkStepRepository { get; set; }

        [Transaction]
        public int Create(WorkStep entity)
        {
            return WorkStepRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public WorkStep GetById(int id)
        {
            return WorkStepRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<WorkStep> GetAll()
        {
            return WorkStepRepository.LinqQuery.ToList();
        }

        [Transaction(ReadOnly = true)]
        public WorkStep GetByUser(User user)
        {
            return WorkStepRepository.LinqQuery.Where(o => o.Users.Contains(user)).FirstOrDefault();
        }

        [Transaction]
        public void Update(WorkStep entity)
        {
            WorkStepRepository.Update(entity);
        }

        [Transaction]
        public void Delete(WorkStep entity)
        {
            WorkStepRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<WorkStep> entitys)
        {
            foreach (WorkStep entity in entitys)
            {
                WorkStepRepository.Delete(entity);
            }
        }
    }
}
