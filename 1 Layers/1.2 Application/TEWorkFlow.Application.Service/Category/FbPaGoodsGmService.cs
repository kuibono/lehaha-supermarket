using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;

namespace TEWorkFlow.Application.Service.Category
{
    public class FbPaGoodsGmService : IFbPaGoodsGmService
    {

        public IRepositoryGUID<FbPaGoodsGm> EntityRepository { get; set; }

        [Transaction]
        public string Create(FbPaGoodsGm entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbPaGoodsGm GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbPaGoodsGm> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }

        [Transaction]
        public IList<FbPaGoodsGm> GetByGbId(string GbId)
        {
            var result = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(GbId) == false)
            {
                result = result.Where(p => p.GbCode == GbId);
            }

            return result.ToList();
        }


        [Transaction]
        public void Update(FbPaGoodsGm entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbPaGoodsGm entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbPaGoodsGm> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbPaGoodsGm> Search(SearchDtoBase<FbPaGoodsGm> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.GbCode) == false)
                {
                    q = q.Where(p => p.GbCode.Contains(c.entity.GbCode));
                }
                if (string.IsNullOrEmpty(c.entity.GmName) == false)
                {
                    q = q.Where(p => p.GmName.Contains(c.entity.GmName));
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
