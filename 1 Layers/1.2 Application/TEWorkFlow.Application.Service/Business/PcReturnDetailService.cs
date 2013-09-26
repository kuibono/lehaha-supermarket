using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Application.Service.Business
{
    public class PcReturnDetailService : IPcReturnDetailService
    {

        public IRepositoryGUID<PcReturnDetail> EntityRepository { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsRepository { get; set; }

        [Transaction]
        public string Create(PcReturnDetail entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public PcReturnDetail GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<PcReturnDetail> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(PcReturnDetail entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(PcReturnDetail entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<PcReturnDetail> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        [Transaction]
        public IList<PcReturnDetail> GetDetailsByManageId(string manageId)
        {
            var q = from l in EntityRepository.LinqQuery where l.RtNumber == manageId select l;
            if (q.Count() == 0)
            {
                return new List<PcReturnDetail>();
            }
            var result = (from l in EntityRepository.LinqQuery where l.RtNumber == manageId select l).ToList();
            FillGoodsName(result);
            return result;
        }

        private void FillGoodsName(IList<PcReturnDetail> details)
        {
            var goods = GoodsRepository.LinqQuery.ToList();
            for (int i = 0; i < details.Count; i++)
            {
                string goodsId = details[i].GoodsCode;
                string goodsName = goods.Where(p => p.Id == goodsId).Count() > 0 ? goods.Where(p => p.Id == goodsId).First().GoodsName : "";

                details[i].GoodsName = goodsName;
            }
        }

        [Transaction]
        public SearchResult<PcReturnDetail> Search(SearchDtoBase<PcReturnDetail> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.RtNumber) == false)
                {
                    q = q.Where(p => p.RtNumber.Contains(c.entity.RtNumber));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsCode) == false)
                {
                    q = q.Where(p => p.GoodsCode.Contains(c.entity.GoodsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsBarCode) == false)
                {
                    q = q.Where(p => p.GoodsBarCode.Contains(c.entity.GoodsBarCode));
                }
                if (string.IsNullOrEmpty(c.entity.Specification) == false)
                {
                    q = q.Where(p => p.Specification.Contains(c.entity.Specification));
                }
                if (string.IsNullOrEmpty(c.entity.PackUnitCode) == false)
                {
                    q = q.Where(p => p.PackUnitCode.Contains(c.entity.PackUnitCode));
                }
                if (c.entity.PackQty > 0)
                {
                    q = q.Where(p => p.PackQty == c.entity.PackQty);
                }

                if (c.entity.PackCoef > 0)
                {
                    q = q.Where(p => p.PackCoef == c.entity.PackCoef);
                }

                if (c.entity.ReturnQty > 0)
                {
                    q = q.Where(p => p.ReturnQty == c.entity.ReturnQty);
                }

                if (c.entity.SalePrice > 0)
                {
                    q = q.Where(p => p.SalePrice == c.entity.SalePrice);
                }

                if (c.entity.PurchasePrice > 0)
                {
                    q = q.Where(p => p.PurchasePrice == c.entity.PurchasePrice);
                }

                if (c.entity.NontaxPurchasePrice > 0)
                {
                    q = q.Where(p => p.NontaxPurchasePrice == c.entity.NontaxPurchasePrice);
                }

                if (c.entity.ReturnMoney > 0)
                {
                    q = q.Where(p => p.ReturnMoney == c.entity.ReturnMoney);
                }

                if (c.entity.NontaxReturnMoney > 0)
                {
                    q = q.Where(p => p.NontaxReturnMoney == c.entity.NontaxReturnMoney);
                }

                if (c.entity.SaleMoney > 0)
                {
                    q = q.Where(p => p.SaleMoney == c.entity.SaleMoney);
                }

                if (c.entity.NontaxSaleMoney > 0)
                {
                    q = q.Where(p => p.NontaxSaleMoney == c.entity.NontaxSaleMoney);
                }


            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<PcReturnDetail> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.RtNumber.Contains(key)
                    || l.GoodsCode.Contains(key)
                    || l.GoodsBarCode.Contains(key)
                    || l.Specification.Contains(key)
                    || l.PackUnitCode.Contains(key)
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