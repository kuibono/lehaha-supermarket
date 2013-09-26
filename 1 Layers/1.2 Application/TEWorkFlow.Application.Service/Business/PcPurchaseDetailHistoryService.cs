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
    public class PcPurchaseDetailHistoryService : IPcPurchaseDetailHistoryService
    {

        public IRepositoryGUID<PcPurchaseDetailHistory> EntityRepository { get; set; }

        public IRepositoryGUID<GoodsArchives> GoodsRepository { get; set; }

        [Transaction]
        public string Create(PcPurchaseDetailHistory entity)
        {
            entity.AddTime = DateTime.Now;
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public PcPurchaseDetailHistory GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<PcPurchaseDetailHistory> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(PcPurchaseDetailHistory entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(PcPurchaseDetailHistory entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<PcPurchaseDetailHistory> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        [Transaction]
        public IList<PcPurchaseDetailHistory> GetDetailsByManageId(string manageId)
        {
            var q = from l in EntityRepository.LinqQuery where l.ManageId == manageId select l;
            if (q.Count() == 0)
            {
                return new List<PcPurchaseDetailHistory>();
            }
            var result= (from l in EntityRepository.LinqQuery where l.ManageId == manageId select l).ToList();
            var goodsIds = result.Select(p => p.GoodsCode).ToArray();
            var goods = GoodsRepository.LinqQuery.Where(p => goodsIds.Contains(p.Id)).ToList();
            FillGoodsName(result, goods);
            return result;
        }
        private void FillGoodsName(IList<PcPurchaseDetailHistory> details, List<GoodsArchives> goods = null)
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
        private void FillGoodsName(IList<PcPurchaseDetailHistory> details)
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
        public SearchResult<PcPurchaseDetailHistory> Search(SearchDtoBase<PcPurchaseDetailHistory> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
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
                if (c.entity.OfferMin > 0)
                {
                    q = q.Where(p => p.OfferMin == c.entity.OfferMin);
                }

                if (c.entity.StockQty > 0)
                {
                    q = q.Where(p => p.StockQty == c.entity.StockQty);
                }

                if (c.entity.OrderQty > 0)
                {
                    q = q.Where(p => p.OrderQty == c.entity.OrderQty);
                }

                if (c.entity.PackQty > 0)
                {
                    q = q.Where(p => p.PackQty == c.entity.PackQty);
                }

                if (c.entity.PackCoef > 0)
                {
                    q = q.Where(p => p.PackCoef == c.entity.PackCoef);
                }

                if (c.entity.PurchaseQty > 0)
                {
                    q = q.Where(p => p.PurchaseQty == c.entity.PurchaseQty);
                }

                if (c.entity.PutinQty > 0)
                {
                    q = q.Where(p => p.PutinQty == c.entity.PutinQty);
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

                if (c.entity.PurchaseMoney > 0)
                {
                    q = q.Where(p => p.PurchaseMoney == c.entity.PurchaseMoney);
                }

                if (c.entity.NontaxPurchaseMoney > 0)
                {
                    q = q.Where(p => p.NontaxPurchaseMoney == c.entity.NontaxPurchaseMoney);
                }

                if (string.IsNullOrEmpty(c.entity.ManageId) == false)
                {
                    q = q.Where(p => p.ManageId.Contains(c.entity.ManageId));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
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
