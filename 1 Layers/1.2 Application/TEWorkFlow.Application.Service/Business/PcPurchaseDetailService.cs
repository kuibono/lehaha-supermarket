﻿using System;
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
    public class PcPurchaseDetailService : IPcPurchaseDetailService
    {

        public IRepositoryGUID<PcPurchaseDetail> EntityRepository { get; set; }

        [Transaction]
        public string Create(PcPurchaseDetail entity)
        {
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

                if (string.IsNullOrEmpty(c.entity.SysGuid) == false)
                {
                    q = q.Where(p => p.SysGuid.Contains(c.entity.SysGuid));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex - 1) * c.pageSize).Take(c.pageSize);
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