using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Core.DataPage;
using NSH.Authorization.Domain;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class InvoiceService : IInvoiceService
    {
        public IRepository<Invoice> InvoiceRepository { get; set; }

        public IRepository<InvoicePoint> InvoicePointRepository { get; set; }

        public IRepository<WorkStep> WorkStepRepository { get; set; }

        public IRepository<CheckPoint> CheckPointRepository { get; set; }

        [Transaction]
        public int Create(Invoice entity)
        {
            return InvoiceRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public Invoice GetById(int id)
        {
            return InvoiceRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<Invoice> GetAll()
        {
            return InvoiceRepository.LinqQuery.ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<Invoice> GetListByUser(User user, IDataPage dataPage = null, IDataOrder dataOrder = null, bool onlyUsersPoints = false)
        {
            var query = InvoiceRepository.LinqQuery;
            query = query.Where(
                o => WorkStepRepository.LinqQuery
                    .Where(w => w.Users.Any(u => u.Id == user.Id))
                    .Any(s => s.Id == o.WorkStep.Id));
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
            var list = query.ToList();
            if (onlyUsersPoints)
            {
                foreach (var l in list)
                {
                    l.Points = l.Points.Where(o => o.CheckPoint.Step.Users.Any(u => u.Id == user.Id)).ToList();
                }
            }
            InvoiceRepository.Clear();
            return list;
        }

        [Transaction(ReadOnly = true)]
        public IList<Invoice> GetListByKeyword(string field, string keyword, IDataPage dataPage = null, IDataOrder dataOrder = null)
        {
            return GetListByKeyword(new Dictionary<string, string>() { { field ?? String.Empty, keyword } }, dataPage, dataOrder);
        }

        [Transaction(ReadOnly = true)]
        public IList<Invoice> GetListByKeyword(IDictionary<string, string> fieldValues, IDataPage dataPage = null, IDataOrder dataOrder = null)
        {
            var query = InvoiceRepository.LinqQuery;

            foreach (KeyValuePair<string, string> fieldValue in fieldValues)
            {
                string field = fieldValue.Key;
                string keyword = fieldValue.Value;
                if (!String.IsNullOrWhiteSpace(field) && keyword != null)
                {
                    if (keyword == "")
                    {
                        query = query.Where(o => o.Points.Any(p => (p.Text == null || p.Text == "") && p.CheckPoint.ItemName == field));
                    }
                    else
                    {
                        query = query.Where(o => o.Points.Any(p => p.Text.Contains(keyword) && p.CheckPoint.ItemName == field));
                    }
                }
            }

            if (dataPage != null)
            {
                query = query.CountAndPage(dataPage);
            }
            return query.ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<Invoice> GetListByDto(InvoiceSearchDto searchDto, IDataPage dataPage = null, IDataOrder dataOrder = null)
        {
            var query = InvoiceRepository.LinqQuery;

            if (searchDto.stepid.HasValue && searchDto.stepid.Value > 0)
            {
                query = query.Where(o => o.WorkStep.Id == searchDto.stepid.Value);
            }
            if (searchDto.userid.HasValue && searchDto.userid.Value > 0)
            {
                query = query.Where(o => o.Points.Any(p => p.UpdateUser.Id == searchDto.userid.Value));
            }

            if (!String.IsNullOrWhiteSpace(searchDto.field))
            {
                if ((searchDto.fieldType ?? String.Empty).ToLower() == "date")
                {
                    if (searchDto.dateStart.HasValue || searchDto.dateEnd.HasValue)
                    {
                        var list = query.ToList();
                        var newList = new List<Invoice>();
                        foreach (var invoice in list)
                        {
                            foreach (var p in invoice.Points)
                            {
                                if (p.CheckPoint.ItemName == searchDto.field &&
                                    p.CheckPoint.CheckPointType == CheckPointType.Date)
                                {
                                    DateTime date;
                                    if (DateTime.TryParse(p.Text, out date))
                                    {
                                        if (!newList.Contains(invoice)
                                            && (!searchDto.dateStart.HasValue || date >= searchDto.dateStart.Value)
                                            && (!searchDto.dateEnd.HasValue || date <= searchDto.dateStart.Value))
                                        {
                                            newList.Add(invoice);
                                        }
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine(p.Text);
                                    }
                                }
                            }
                        }
                        query = newList.AsQueryable();
                        //if (searchDto.dateStart.HasValue)
                        //{
                        //    query = query.Where(o => o.Points.Any(p => 
                        //        p.CheckPoint.CheckPointType == CheckPointType.Date 
                        //        &&  Convert.ToDateTime(p.Text) >= searchDto.dateStart 
                        //        && p.CheckPoint.ItemName == searchDto.field));
                        //}
                        //if (searchDto.dateEnd.HasValue)
                        //{
                        //    query = query.Where(o => o.Points.Any(p => 
                        //        p.CheckPoint.CheckPointType == CheckPointType.Date 
                        //        && Convert.ToDateTime(p.Text) <= searchDto.dateEnd 
                        //        && p.CheckPoint.ItemName == searchDto.field));
                        //}
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(searchDto.keyword))
                    {
                        query = query.Where(o => o.Points.Any(p => (p.Text == null || p.Text == "") && p.CheckPoint.ItemName == searchDto.field));
                    }
                    else
                    {
                        query = query.Where(o => o.Points.Any(p => p.Text.Contains(searchDto.keyword) && p.CheckPoint.ItemName == searchDto.field));
                    }
                }
            }

            if (dataPage != null)
            {
                query = query.CountAndPage(dataPage);
            }
            return query.ToList();
        }

        [Transaction]
        public void Update(Invoice entity)
        {
            InvoiceRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Invoice entity)
        {
            InvoiceRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Invoice> entitys)
        {
            foreach (Invoice entity in entitys)
            {
                InvoiceRepository.Delete(entity);
            }
        }
        [Transaction]
        public void CheckFinished(int id)
        {
            var items = from i in InvoicePointRepository.LinqQuery
                        from c in CheckPointRepository.LinqQuery
                        where i.CheckPoint.Id == c.Id
                        orderby c.Id
                        select i;
            //InvoicePointRepository.LinqQuery.Where(p => p.Invoice.Id == id);
            var statuses = WorkStepRepository.LinqQuery.ToList();


            var q = GetById(id);

            if (items.ToList().Where(p => p.IsChecked == false).Count() == 0)
            {
                q.WorkStep = WorkStepRepository.LinqQuery.OrderByDescending(p => p.Id).First();
            }

            foreach (var item in items)
            {
                if (item.IsChecked == false)
                {
                    q.WorkStep = item.CheckPoint.Step;
                    break;
                }
            }

            Update(q);
        }
    }
}
