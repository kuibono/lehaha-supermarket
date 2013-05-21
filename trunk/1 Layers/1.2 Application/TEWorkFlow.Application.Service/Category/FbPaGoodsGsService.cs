using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Application.Service.Sys;
using NSH.VSTO;
namespace TEWorkFlow.Application.Service.Category
{
    public class FbPaGoodsGsService : IFbPaGoodsGsService
    {

        public IRepositoryGUID<FbPaGoodsGs> EntityRepository { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }

        [Transaction]
        public string Create(FbPaGoodsGs entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbPaGoodsGs GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbPaGoodsGs> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }

        [Transaction]
        public void Save(FbPaGoodsGs entity)
        {
            bool add = false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = GenarateId();
            }
            else
            {
                if (EntityRepository.LinqQuery.Count(p => p.Id == entity.Id) > 0)
                {
                    add = false;
                }
                else
                {
                    add = true;
                }
            }

            if (add)
            {
                EntityRepository.Save(entity);
            }
            else
            {
                EntityRepository.Update(entity);
            }
        }

        [Transaction]
        public IList<FbPaGoodsGs> GetByGbId(string GmId)
        {
            var result = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(GmId) == false)
            {
                result = result.Where(p => p.GmCode == GmId);
            }

            return result.ToList();
        }

        [Transaction]
        public void Update(FbPaGoodsGs entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbPaGoodsGs entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbPaGoodsGs> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbPaGoodsGs> Search(SearchDtoBase<FbPaGoodsGs> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.GmCode) == false)
                {
                    q = q.Where(p => p.GmCode.Contains(c.entity.GmCode));
                }
                if (string.IsNullOrEmpty(c.entity.GsName) == false)
                {
                    q = q.Where(p => p.GsName.Contains(c.entity.GsName));
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

        [Transaction]
        public string GenarateId()
        {
            var setting = FbPaBaseSetService.Get();
            int maxId = (from l in EntityRepository.LinqQuery orderby l.Id descending select l).First().Id.ToInt32();
            return (maxId + 1).ToString().FillByStrings('0', setting.GoodsGsLen.ToInt32());
        }
    }
}