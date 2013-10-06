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
using NSH.VSTO;

namespace TEWorkFlow.Application.Service.Business
{
    public class PcPurchaseDetailService : IPcPurchaseDetailService
    {

        public IRepositoryGUID<PcPurchaseDetail> EntityRepository { get; set; }

        public IRepositoryGUID<GoodsArchives> GoodsRepository { get; set; }

        [Transaction]
        public string Create(PcPurchaseDetail entity)
        {
            entity.AddTime = DateTime.Now;
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public PcPurchaseDetail GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<PcPurchaseDetail> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(PcPurchaseDetail entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(PcPurchaseDetail entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<PcPurchaseDetail> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        [Transaction]
        public IList<PcPurchaseDetail> GetDetailsByManageId(string manageId)
        {
            //var allGoods = GoodsRepository.LinqQuery.AsCache("_allGoods");
            var q = from l in EntityRepository.LinqQuery where l.ManageId == manageId select l;
            if (q.Count() == 0)
            {
                return new List<PcPurchaseDetail>();
            }
            var result = (from l in EntityRepository.LinqQuery where l.ManageId == manageId select l).ToList();
            //var goodsIds = result.Select(p => p.GoodsCode).ToArray();
            //var goods = allGoods.Where(p => goodsIds.Contains(p.Id)).ToList();
            FillGoodsName(result);
            return result;
        }

        private void FillGoodsName(IList<PcPurchaseDetail> details)
        {
            var goods = GoodsRepository.LinqQuery.Select(p => new StringKeyValuePair { Key = p.Id, Value = p.GoodsName }).AsCache("_allGoodsNames");
            //if (goods == null)
            //{
            //    goods = GoodsRepository.LinqQuery.ToList();
            //}
            for (int i = 0; i < details.Count; i++)
            {
                string goodsId = details[i].GoodsCode;
                string goodsName = goods.Where(p => p.Key == goodsId).Count() > 0 ? goods.Where(p => p.Key == goodsId).First().Value : "";

                details[i].GoodsName = goodsName;
            }
        }

        [Transaction]
        public SearchResult<PcPurchaseDetail> Search(SearchDtoBase<PcPurchaseDetail> c)
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
