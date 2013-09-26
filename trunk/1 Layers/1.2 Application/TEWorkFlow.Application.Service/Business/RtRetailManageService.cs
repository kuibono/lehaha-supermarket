using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Application.Service.Business
{
    public class RtRetailManageService : IRtRetailManageService
    {

        public IRepositoryGUID<RtRetailManage> EntityRepository { get; set; }
        public IRepositoryGUID<RtRetailDetail> RtRetailDetailRepository { get; set; }

        [Transaction]
        public string Create(RtRetailManage entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public RtRetailManage GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<RtRetailManage> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }
        public SearchResult<RtRetailManage> SearchReportBySupplier(DateTime? dateS, DateTime? dateE,  string bCode, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.bCode == bCode
                    select l;
            }
            if (dateS <= dateE )
            {
                q = from l in q
                    where l.RtDate >= dateS
                        && l.RtDate <= dateE
                    select l;
            }

            int count = q.Count();

            var result = q.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var statics = RtRetailDetailRepository.LinqQuery.Where(p => p.RtNumber == result[i].Id);
                result[i].detailCount = statics.Count();
                result[i].count = Convert.ToInt32(statics.Sum(p => p.PurchaseQty));
                result[i].amount = statics.Sum(p => p.SaleMoney);
            }

            var sta = new
            {
                count = result.Count,
                detailCount = result.Sum(p => p.detailCount),
                goodscount = result.Sum(p => p.count),
                amount = result.Sum(p => p.amount)
            };
            var searchResult = result.ToSearchResult(count);
            searchResult.Statistics = sta;
            //FillBranchName(result);
            return searchResult;

        }


        [Transaction]
        public void Update(RtRetailManage entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(RtRetailManage entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<RtRetailManage> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<RtRetailManage> Search(SearchDtoBase<RtRetailManage> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (c.entity.SaFlowNumber > 0)
                {
                    q = q.Where(p => p.SaFlowNumber == c.entity.SaFlowNumber);
                }
                if (string.IsNullOrEmpty(c.entity.RtType) == false)
                {
                    q = q.Where(p => p.RtType.Contains(c.entity.RtType));
                }
                if (string.IsNullOrEmpty(c.entity.WhCode) == false)
                {
                    q = q.Where(p => p.WhCode.Contains(c.entity.WhCode));
                }
                if (string.IsNullOrEmpty(c.entity.bCode) == false)
                {
                    q = q.Where(p => p.bCode.Contains(c.entity.bCode));
                }
                if (string.IsNullOrEmpty(c.entity.Posid) == false)
                {
                    q = q.Where(p => p.Posid.Contains(c.entity.Posid));
                }
                if (c.entity.SaleQty > 0)
                {
                    q = q.Where(p => p.SaleQty == c.entity.SaleQty);
                }

                if (c.entity.SaleMoney > 0)
                {
                    q = q.Where(p => p.SaleMoney == c.entity.SaleMoney);
                }

                if (c.entity.DiscountMoney > 0)
                {
                    q = q.Where(p => p.DiscountMoney == c.entity.DiscountMoney);
                }

                if (string.IsNullOrEmpty(c.entity.CardNumber) == false)
                {
                    q = q.Where(p => p.CardNumber.Contains(c.entity.CardNumber));
                }
                if (string.IsNullOrEmpty(c.entity.Cashier) == false)
                {
                    q = q.Where(p => p.Cashier.Contains(c.entity.Cashier));
                }
                if (string.IsNullOrEmpty(c.entity.CashierHigher) == false)
                {
                    q = q.Where(p => p.CashierHigher.Contains(c.entity.CashierHigher));
                }
                if (string.IsNullOrEmpty(c.entity.Salesman) == false)
                {
                    q = q.Where(p => p.Salesman.Contains(c.entity.Salesman));
                }
                if (string.IsNullOrEmpty(c.entity.UnderCounterCode) == false)
                {
                    q = q.Where(p => p.UnderCounterCode.Contains(c.entity.UnderCounterCode));
                }
                if (string.IsNullOrEmpty(c.entity.ReturnNumber) == false)
                {
                    q = q.Where(p => p.ReturnNumber.Contains(c.entity.ReturnNumber));
                }
                if (string.IsNullOrEmpty(c.entity.Comment) == false)
                {
                    q = q.Where(p => p.Comment.Contains(c.entity.Comment));
                }
                if (string.IsNullOrEmpty(c.entity.SysGuid) == false)
                {
                    q = q.Where(p => p.SysGuid.Contains(c.entity.SysGuid));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<RtRetailManage> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.RtType.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.bCode.Contains(key)
                    || l.Posid.Contains(key)
                    || l.CardNumber.Contains(key)
                    || l.Cashier.Contains(key)
                    || l.CashierHigher.Contains(key)
                    || l.Salesman.Contains(key)
                    || l.UnderCounterCode.Contains(key)
                    || l.ReturnNumber.Contains(key)
                    || l.Comment.Contains(key)
                    || l.SysGuid.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            return result.ToList();
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EntityRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (var each in q)
            {
                Delete(each);
            }
        }
    }
}