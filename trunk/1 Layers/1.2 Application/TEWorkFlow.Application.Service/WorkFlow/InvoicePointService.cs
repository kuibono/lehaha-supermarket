using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;
using NSH.Core.Domain;
using Spring.Transaction.Interceptor;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class InvoicePointService : IInvoicePointService
    {
        public IRepository<InvoicePoint> InvoicePointRepository { get; set; }

        [Transaction]
        public int Create(InvoicePoint entity)
        {
            return InvoicePointRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public InvoicePoint GetById(int id)
        {
            return InvoicePointRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<InvoicePoint> GetAll()
        {
            return InvoicePointRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(InvoicePoint entity)
        {
            InvoicePointRepository.Update(entity);
        }

        [Transaction]
        public void Delete(InvoicePoint entity)
        {
            InvoicePointRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<InvoicePoint> entitys)
        {
            foreach (InvoicePoint entity in entitys)
            {
                InvoicePointRepository.Delete(entity);
            }
        }
        [Transaction]
        public InvoicePoint GetByInvoiceIdAndItemName(int id,string itemName)
        {
            return InvoicePointRepository.LinqQuery.Where(p => p.Invoice.Id == id && p.CheckPoint.ItemName.Replace(" ", "") == itemName.Replace(" ", "")).FirstOrDefault();
        }
    }
}
