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
    public class FbGoodsSupplierRelationService : IFbGoodsSupplierRelationService
    {

        public IRepositoryGUID<FbGoodsSupplierRelation> EntityRepository { get; set; }

        [Transaction]
        public string Create(FbGoodsSupplierRelation entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbGoodsSupplierRelation GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbGoodsSupplierRelation> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(FbGoodsSupplierRelation entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbGoodsSupplierRelation entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbGoodsSupplierRelation> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbGoodsSupplierRelation> Search(SearchDtoBase<FbGoodsSupplierRelation> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.bCode) == false)
                {
                    q = q.Where(p => p.bCode.Contains(c.entity.bCode));
                }
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    q = q.Where(p => p.SupCode.Contains(c.entity.SupCode));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<FbGoodsSupplierRelation> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.bCode.Contains(key)
                    || l.SupCode.Contains(key)
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