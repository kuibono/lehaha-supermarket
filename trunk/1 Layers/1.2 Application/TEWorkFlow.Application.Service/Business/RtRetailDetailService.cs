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
    public class RtRetailDetailService : IRtRetailDetailService
    {

        public IRepositoryGUID<RtRetailDetail> EntityRepository { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsRepository { get; set; }
        [Transaction]
        public string Create(RtRetailDetail entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public RtRetailDetail GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<RtRetailDetail> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(RtRetailDetail entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(RtRetailDetail entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<RtRetailDetail> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<RtRetailDetail> Search(SearchDtoBase<RtRetailDetail> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (c.entity.SaFlowNumber > 0)
                {
                    q = q.Where(p => p.SaFlowNumber == c.entity.SaFlowNumber);
                }

                if (string.IsNullOrEmpty(c.entity.RtNumber) == false)
                {
                    q = q.Where(p => p.RtNumber.Contains(c.entity.RtNumber));
                }
                if (string.IsNullOrEmpty(c.entity.sNumber) == false)
                {
                    q = q.Where(p => p.sNumber.Contains(c.entity.sNumber));
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
                if (string.IsNullOrEmpty(c.entity.GoodsCode) == false)
                {
                    q = q.Where(p => p.GoodsCode.Contains(c.entity.GoodsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsBarCode) == false)
                {
                    q = q.Where(p => p.GoodsBarCode.Contains(c.entity.GoodsBarCode));
                }
                if (c.entity.SalePrice > 0)
                {
                    q = q.Where(p => p.SalePrice == c.entity.SalePrice);
                }

                if (c.entity.RealPrice > 0)
                {
                    q = q.Where(p => p.RealPrice == c.entity.RealPrice);
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

                if (c.entity.PurchaseQty > 0)
                {
                    q = q.Where(p => p.PurchaseQty == c.entity.PurchaseQty);
                }

                if (c.entity.pSalePrice > 0)
                {
                    q = q.Where(p => p.pSalePrice == c.entity.pSalePrice);
                }

                if (c.entity.pSaleMoney > 0)
                {
                    q = q.Where(p => p.pSaleMoney == c.entity.pSaleMoney);
                }

                if (c.entity.pDiscountMoney > 0)
                {
                    q = q.Where(p => p.pDiscountMoney == c.entity.pDiscountMoney);
                }

                if (string.IsNullOrEmpty(c.entity.Cashier) == false)
                {
                    q = q.Where(p => p.Cashier.Contains(c.entity.Cashier));
                }
                if (string.IsNullOrEmpty(c.entity.Salesman) == false)
                {
                    q = q.Where(p => p.Salesman.Contains(c.entity.Salesman));
                }
                if (string.IsNullOrEmpty(c.entity.UnderCounterCode) == false)
                {
                    q = q.Where(p => p.UnderCounterCode.Contains(c.entity.UnderCounterCode));
                }
                if (c.entity.PoolRate > 0)
                {
                    q = q.Where(p => p.PoolRate == c.entity.PoolRate);
                }

                if (string.IsNullOrEmpty(c.entity.IfPromotion) == false)
                {
                    q = q.Where(p => p.IfPromotion.Contains(c.entity.IfPromotion));
                }
                if (string.IsNullOrEmpty(c.entity.OwnerType) == false)
                {
                    q = q.Where(p => p.OwnerType.Contains(c.entity.OwnerType));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<RtRetailDetail> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.RtNumber.Contains(key)
                    || l.sNumber.Contains(key)
                    || l.RtType.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.bCode.Contains(key)
                    || l.Posid.Contains(key)
                    || l.GoodsCode.Contains(key)
                    || l.GoodsBarCode.Contains(key)
                    || l.Cashier.Contains(key)
                    || l.Salesman.Contains(key)
                    || l.UnderCounterCode.Contains(key)
                    || l.IfPromotion.Contains(key)
                    || l.OwnerType.Contains(key)
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
        private void FillGoodsName(IList<RtRetailDetail> details, List<GoodsArchives> goods = null)
        {
            if (goods == null)
            {
                goods = GoodsRepository.LinqQuery.ToList();
            }
            for (int i = 0; i < details.Count; i++)
            {
                string goodsId = details[i].GoodsCode;
                string goodsName = goods.Where(p => p.Id == goodsId).Count() > 0 ? goods.Where(p => p.Id == goodsId).First().GoodsName : "";

                details[i].GoodsName = goodsName;
            }
        }

        [Transaction]
        public IList<RtRetailDetail> GetDetailsByManageId(string manageId)
        {
            var q = from l in EntityRepository.LinqQuery where l.RtNumber == manageId select l;
            if (q.Count() == 0)
            {
                return new List<RtRetailDetail>();
            }
            var result = (from l in EntityRepository.LinqQuery where l.RtNumber == manageId select l).ToList();
            var goodsIds = result.Select(p => p.GoodsCode).ToArray();
            var goods = GoodsRepository.LinqQuery.Where(p => goodsIds.Contains(p.Id)).ToList();
            FillGoodsName(result, goods);
            return result;
        }
    }
}
