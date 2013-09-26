using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Application.Service.Archives
{
    public class FbGoodsArchivesBarService : IFbGoodsArchivesBarService
    {

        public IRepositoryGUID<FbGoodsArchivesBar> EntityRepository { get; set; }

        [Transaction]
        public string Create(FbGoodsArchivesBar entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbGoodsArchivesBar GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbGoodsArchivesBar> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(FbGoodsArchivesBar entity)
        {
            EntityRepository.SaveOrUpdate(entity);
        }

        [Transaction]
        public void Delete(FbGoodsArchivesBar entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbGoodsArchivesBar> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbGoodsArchivesBar> Search(SearchDtoBase<FbGoodsArchivesBar> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.GoodsCode) == false)
                {
                    q = q.Where(p => p.GoodsCode.Contains(c.entity.GoodsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsBarName) == false)
                {
                    q = q.Where(p => p.GoodsBarName.Contains(c.entity.GoodsBarName));
                }
                if (string.IsNullOrEmpty(c.entity.PackUnitCode) == false)
                {
                    q = q.Where(p => p.PackUnitCode.Contains(c.entity.PackUnitCode));
                }
                if (c.entity.PackCoef > 0)
                {
                    q = q.Where(p => p.PackCoef == c.entity.PackCoef);
                }

                if (string.IsNullOrEmpty(c.entity.QtyType) == false)
                {
                    q = q.Where(p => p.QtyType.Contains(c.entity.QtyType));
                }
                if (string.IsNullOrEmpty(c.entity.PriceType) == false)
                {
                    q = q.Where(p => p.PriceType.Contains(c.entity.PriceType));
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

                if (string.IsNullOrEmpty(c.entity.IfMainBar) == false)
                {
                    q = q.Where(p => p.IfMainBar.Contains(c.entity.IfMainBar));
                }
                if (string.IsNullOrEmpty(c.entity.IfExamine) == false)
                {
                    q = q.Where(p => p.IfExamine.Contains(c.entity.IfExamine));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<FbGoodsArchivesBar> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.GoodsCode.Contains(key)
                    || l.GoodsBarName.Contains(key)
                    || l.PackUnitCode.Contains(key)
                    || l.QtyType.Contains(key)
                    || l.PriceType.Contains(key)
                    || l.IfMainBar.Contains(key)
                    || l.IfExamine.Contains(key)
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
