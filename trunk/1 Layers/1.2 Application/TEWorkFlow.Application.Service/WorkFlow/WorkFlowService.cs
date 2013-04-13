using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using Spring.Transaction.Interceptor;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class WorkFlowService:IWorkFlowService
    {
        public IRepository<TEWorkFlow.Domain.WorkFlow.WorkFlow> WorkFlowRepository { get; set; }

        [Transaction]
        public int Create(TEWorkFlow.Domain.WorkFlow.WorkFlow entity)
        {
            return WorkFlowRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public TEWorkFlow.Domain.WorkFlow.WorkFlow GetById(int id)
        {
            return WorkFlowRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<TEWorkFlow.Domain.WorkFlow.WorkFlow> GetAll()
        {
            return WorkFlowRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(TEWorkFlow.Domain.WorkFlow.WorkFlow entity)
        {
            WorkFlowRepository.Update(entity);
        }

        [Transaction]
        public void Delete(TEWorkFlow.Domain.WorkFlow.WorkFlow entity)
        {
            WorkFlowRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<TEWorkFlow.Domain.WorkFlow.WorkFlow> entitys)
        {
            foreach (TEWorkFlow.Domain.WorkFlow.WorkFlow entity in entitys)
            {
                WorkFlowRepository.Delete(entity);
            }
        }
    }
}
