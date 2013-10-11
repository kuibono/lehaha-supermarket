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
    public class FbGoodsArchivesSupplierService : IFbGoodsArchivesSupplierService
    {

        public IRepositoryGUID<FbGoodsArchivesSupplier> EntityRepository { get; set; }

        [Transaction]
        public string Create(FbGoodsArchivesSupplier entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbGoodsArchivesSupplier GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbGoodsArchivesSupplier> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(FbGoodsArchivesSupplier entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbGoodsArchivesSupplier entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbGoodsArchivesSupplier> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbGoodsArchivesSupplier> Search(SearchDtoBase<FbGoodsArchivesSupplier> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.GoodsCode) == false)
                {
                    q = q.Where(p => p.GoodsCode.Contains(c.entity.GoodsCode));
                }
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    q = q.Where(p => p.SupCode.Contains(c.entity.SupCode));
                }
                if (string.IsNullOrEmpty(c.entity.SupName) == false)
                {
                    q = q.Where(p => p.SupName.Contains(c.entity.SupName));
                }
                if (string.IsNullOrEmpty(c.entity.PyCode) == false)
                {
                    q = q.Where(p => p.PyCode.Contains(c.entity.PyCode));
                }
                if (string.IsNullOrEmpty(c.entity.OpCode) == false)
                {
                    q = q.Where(p => p.OpCode.Contains(c.entity.OpCode));
                }
                if (c.entity.PoolRate > 0)
                {
                    q = q.Where(p => p.PoolRate == c.entity.PoolRate);
                }

                if (string.IsNullOrEmpty(c.entity.OfferMode) == false)
                {
                    q = q.Where(p => p.OfferMode.Contains(c.entity.OfferMode));
                }
                if (c.entity.OfferMin > 0)
                {
                    q = q.Where(p => p.OfferMin == c.entity.OfferMin);
                }

                if (c.entity.InputTax > 0)
                {
                    q = q.Where(p => p.InputTax == c.entity.InputTax);
                }

                if (c.entity.PurchasePrice > 0)
                {
                    q = q.Where(p => p.PurchasePrice == c.entity.PurchasePrice);
                }

                if (c.entity.NontaxPurchasePrice > 0)
                {
                    q = q.Where(p => p.NontaxPurchasePrice == c.entity.NontaxPurchasePrice);
                }

                if (string.IsNullOrEmpty(c.entity.IfMainSupplier) == false)
                {
                    q = q.Where(p => p.IfMainSupplier.Contains(c.entity.IfMainSupplier));
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
        public IList<FbGoodsArchivesSupplier> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.GoodsCode.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.SupName.Contains(key)
                    || l.PyCode.Contains(key)
                    || l.OpCode.Contains(key)
                    || l.OfferMode.Contains(key)
                    || l.IfMainSupplier.Contains(key)
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

        public IList<FbGoodsArchivesSupplier> GetByGoodsCode(string goodsCode)
        {
            return EntityRepository.LinqQuery.Where(p => p.GoodsCode == goodsCode).ToList();
        }

        public IList<FbGoodsArchivesSupplier> GetBySupCode(string supCode)
        {
            return EntityRepository.LinqQuery.Where(p => p.SupCode == supCode).ToList();
        }
    }
}
