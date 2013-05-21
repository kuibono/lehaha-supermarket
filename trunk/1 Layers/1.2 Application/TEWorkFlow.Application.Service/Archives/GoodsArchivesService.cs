﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Application.Service.Sys;
using NSH.VSTO;

namespace TEWorkFlow.Application.Service.Archives
{
    public class GoodsArchivesService : IGoodsArchivesService
    {

        public IRepositoryGUID<GoodsArchives> EntityRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGb> GbRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGm> GmRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGs> GsRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGl> GlRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> SupplierRepository { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }

        [Transaction]
        public string Create(GoodsArchives entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public GoodsArchives GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<GoodsArchives> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(GoodsArchives entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(GoodsArchives entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<GoodsArchives> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        private void FillClassName(IList<GoodsArchives> entitys)
        {
            var gbs = GbRepository.LinqQuery.ToList();
            var gms = GmRepository.LinqQuery.ToList();
            var gss = GsRepository.LinqQuery.ToList();
            var gls = GlRepository.LinqQuery.ToList();

            foreach (var entity in entitys)
            {
                var gb = from l in gbs where l.Id == entity.GbCode select l;
                if (gb.Count() > 0)
                {
                    entity.GbName = gb.First().GbName;
                }
                var gm = from l in gms where l.Id == entity.GmCode select l;
                if (gm.Count() > 0)
                {
                    entity.GmName = gm.First().GmName;
                }
                var gs = from l in gss where l.Id == entity.GsCode select l;
                if (gs.Count() > 0)
                {
                    entity.GsName = gs.First().GsName;
                }

                var gl = from l in gls where l.Id == entity.GlCode select l;
                if (gl.Count() > 0)
                {
                    entity.GlName = gl.First().GlName;
                }

            }
        }

        private void FillSupInfo(IList<GoodsArchives> goods)
        {
            var sups = SupplierRepository.LinqQuery.ToList();
            for (int i = 0; i < goods.Count; i++)
            {
                var sup = sups.Where(p => p.Id == goods[i].SupCode);
                if (sup.Count() == 0)
                {
                    goods[i].SupName = "";
                    goods[i].SupTel = "";
                }
                else
                {
                    goods[i].SupName = sup.First().SupName;
                    goods[i].SupTel = sup.First().ContactPhone;
                }
            }
        }

        [Transaction]
        public SearchResult<GoodsArchives> Search(SearchDtoBase<GoodsArchives> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.GoodsBarCode) == false)
                {
                    q = q.Where(p => p.GoodsBarCode.Contains(c.entity.GoodsBarCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsSubCode) == false)
                {
                    q = q.Where(p => p.GoodsSubCode.Contains(c.entity.GoodsSubCode));
                }
                if (string.IsNullOrEmpty(c.entity.GbCode) == false)
                {
                    q = q.Where(p => p.GbCode.Contains(c.entity.GbCode));
                }
                if (string.IsNullOrEmpty(c.entity.GmCode) == false)
                {
                    q = q.Where(p => p.GmCode.Contains(c.entity.GmCode));
                }
                if (string.IsNullOrEmpty(c.entity.GsCode) == false)
                {
                    q = q.Where(p => p.GsCode.Contains(c.entity.GsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GlCode) == false)
                {
                    q = q.Where(p => p.GlCode.Contains(c.entity.GlCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsType) == false)
                {
                    q = q.Where(p => p.GoodsType.Contains(c.entity.GoodsType));
                }
                if (string.IsNullOrEmpty(c.entity.CheckMode) == false)
                {
                    q = q.Where(p => p.CheckMode.Contains(c.entity.CheckMode));
                }
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    q = q.Where(p => p.SupCode.Contains(c.entity.SupCode));
                }
                if (string.IsNullOrEmpty(c.entity.OpCode) == false)
                {
                    q = q.Where(p => p.OpCode.Contains(c.entity.OpCode));
                }
                if (c.entity.PoolRate > 0)
                {
                    q = q.Where(p => p.PoolRate == c.entity.PoolRate);
                }

                if (string.IsNullOrEmpty(c.entity.GoodsName) == false)
                {
                    q = q.Where(p => p.GoodsName.Contains(c.entity.GoodsName));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsSubName) == false)
                {
                    q = q.Where(p => p.GoodsSubName.Contains(c.entity.GoodsSubName));
                }
                if (string.IsNullOrEmpty(c.entity.PyCode) == false)
                {
                    q = q.Where(p => p.PyCode.Contains(c.entity.PyCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsState) == false)
                {
                    q = q.Where(p => p.GoodsState.Contains(c.entity.GoodsState));
                }
                if (string.IsNullOrEmpty(c.entity.ProducingArea) == false)
                {
                    q = q.Where(p => p.ProducingArea.Contains(c.entity.ProducingArea));
                }
                if (string.IsNullOrEmpty(c.entity.ArticleNumber) == false)
                {
                    q = q.Where(p => p.ArticleNumber.Contains(c.entity.ArticleNumber));
                }
                if (string.IsNullOrEmpty(c.entity.Specification) == false)
                {
                    q = q.Where(p => p.Specification.Contains(c.entity.Specification));
                }
                if (string.IsNullOrEmpty(c.entity.ShelfLife) == false)
                {
                    q = q.Where(p => p.ShelfLife.Contains(c.entity.ShelfLife));
                }
                if (string.IsNullOrEmpty(c.entity.PackUnitCode) == false)
                {
                    q = q.Where(p => p.PackUnitCode.Contains(c.entity.PackUnitCode));
                }
                if (string.IsNullOrEmpty(c.entity.OfferMode) == false)
                {
                    q = q.Where(p => p.OfferMode.Contains(c.entity.OfferMode));
                }
                if (c.entity.PackCoef > 0)
                {
                    q = q.Where(p => p.PackCoef == c.entity.PackCoef);
                }

                if (c.entity.OfferMin > 0)
                {
                    q = q.Where(p => p.OfferMin == c.entity.OfferMin);
                }

                if (c.entity.InputTax > 0)
                {
                    q = q.Where(p => p.InputTax == c.entity.InputTax);
                }

                if (c.entity.OutputTax > 0)
                {
                    q = q.Where(p => p.OutputTax == c.entity.OutputTax);
                }

                if (c.entity.StockUpperLimit > 0)
                {
                    q = q.Where(p => p.StockUpperLimit == c.entity.StockUpperLimit);
                }

                if (c.entity.StockLowerLimit > 0)
                {
                    q = q.Where(p => p.StockLowerLimit == c.entity.StockLowerLimit);
                }

                if (string.IsNullOrEmpty(c.entity.UnderFloorCode) == false)
                {
                    q = q.Where(p => p.UnderFloorCode.Contains(c.entity.UnderFloorCode));
                }
                if (string.IsNullOrEmpty(c.entity.UnderCounterCode) == false)
                {
                    q = q.Where(p => p.UnderCounterCode.Contains(c.entity.UnderCounterCode));
                }
                if (string.IsNullOrEmpty(c.entity.CheckUnitCode) == false)
                {
                    q = q.Where(p => p.CheckUnitCode.Contains(c.entity.CheckUnitCode));
                }
                if (c.entity.PurchasePrice > 0)
                {
                    q = q.Where(p => p.PurchasePrice == c.entity.PurchasePrice);
                }

                if (c.entity.NontaxPurchasePrice > 0)
                {
                    q = q.Where(p => p.NontaxPurchasePrice == c.entity.NontaxPurchasePrice);
                }

                if (c.entity.AvgCost > 0)
                {
                    q = q.Where(p => p.AvgCost == c.entity.AvgCost);
                }

                if (c.entity.NontaxAvgCost > 0)
                {
                    q = q.Where(p => p.NontaxAvgCost == c.entity.NontaxAvgCost);
                }

                if (c.entity.GrossRate > 0)
                {
                    q = q.Where(p => p.GrossRate == c.entity.GrossRate);
                }

                if (c.entity.SalePrice > 0)
                {
                    q = q.Where(p => p.SalePrice == c.entity.SalePrice);
                }

                if (c.entity.VipPrice > 0)
                {
                    q = q.Where(p => p.VipPrice == c.entity.VipPrice);
                }

                if (c.entity.TradePrice > 0)
                {
                    q = q.Where(p => p.TradePrice == c.entity.TradePrice);
                }

                if (c.entity.PushRate > 0)
                {
                    q = q.Where(p => p.PushRate == c.entity.PushRate);
                }

                if (string.IsNullOrEmpty(c.entity.Operator) == false)
                {
                    q = q.Where(p => p.Operator.Contains(c.entity.Operator));
                }
                if (string.IsNullOrEmpty(c.entity.Assessor) == false)
                {
                    q = q.Where(p => p.Assessor.Contains(c.entity.Assessor));
                }
                if (string.IsNullOrEmpty(c.entity.IfExamine) == false)
                {
                    q = q.Where(p => p.IfExamine.Contains(c.entity.IfExamine));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex - 1) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillClassName(result);
            FillSupInfo(result);
            return result.ToSearchResult(count);
        }

        public IList<GoodsArchives> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.GoodsBarCode.Contains(key)
                    || l.GoodsSubCode.Contains(key)
                    || l.GbCode.Contains(key)
                    || l.GmCode.Contains(key)
                    || l.GsCode.Contains(key)
                    || l.GlCode.Contains(key)
                    || l.GoodsType.Contains(key)
                    || l.CheckMode.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.OpCode.Contains(key)
                    || l.GoodsName.Contains(key)
                    || l.GoodsSubName.Contains(key)
                    || l.PyCode.Contains(key)
                    || l.GoodsState.Contains(key)
                    || l.ProducingArea.Contains(key)
                    || l.ArticleNumber.Contains(key)
                    || l.Specification.Contains(key)
                    || l.ShelfLife.Contains(key)
                    || l.PackUnitCode.Contains(key)
                    || l.OfferMode.Contains(key)
                    || l.UnderFloorCode.Contains(key)
                    || l.UnderCounterCode.Contains(key)
                    || l.CheckUnitCode.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillSupInfo(result);
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

        [Transaction]
        public string GenarateGbCode()
        {
            var setting = FbPaBaseSetService.Get();
            int maxId = (from l in EntityRepository.LinqQuery orderby l.GoodsSubCode descending select l).First().GoodsSubCode.ToInt32();
            return (maxId + 1).ToString().FillByStrings('0', setting.SupLen.ToInt32());
        }

        [Transaction]
        public string GenarateId(GoodsArchives entity)
        {
            return entity.GbCode + entity.GmCode + entity.GsCode + entity.GlCode + entity.GoodsSubCode;
        }
    }
}
