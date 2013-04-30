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
    public class FbPaGoodsGlService : IFbPaGoodsGlService
    {

        public IRepositoryGUID<FbPaGoodsGl> EntityRepository { get; set; }

        [Transaction]
        public string Create(FbPaGoodsGl entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbPaGoodsGl GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbPaGoodsGl> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }
        [Transaction]
        public IList<FbPaGoodsGl> GetByGsId(string GsId)
        {
            var result = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(GsId) == false)
            {
                result = result.Where(p => p.GsCode == GsId);
            }

            return result.ToList();
        }

        [Transaction]
        public void Update(FbPaGoodsGl entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbPaGoodsGl entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbPaGoodsGl> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbPaGoodsGl> Search(SearchDtoBase<FbPaGoodsGl> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.GsCode) == false)
                {
                    q = q.Where(p => p.GsCode.Contains(c.entity.GsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GlName) == false)
                {
                    q = q.Where(p => p.GlName.Contains(c.entity.GlName));
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
