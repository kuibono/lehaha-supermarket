using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;
using NSH.VSTO;
using TEWorkFlow.Application.Service.Sys;
using TEWorkFlow.Domain.Sys;
namespace TEWorkFlow.Application.Service.Category
{
    public class FbPaGoodsGlService : IFbPaGoodsGlService
    {

        public IRepositoryGUID<FbPaGoodsGl> EntityRepository { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }
        [Transaction]
        public string Create(FbPaGoodsGl entity)
        {
            string id = EntityRepository.Save(entity);
            TfDataDownloadService.AddDownload("fb_pa_goods_gl", id);
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = id, DownloadTablename = "fb_pa_goods_gl" });
            return id;
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
        public void Save(FbPaGoodsGl entity)
        {
            bool add = false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = GenarateId(entity.GsCode);
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
            TfDataDownloadService.AddDownload("fb_pa_goods_gl", entity.Id);
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = entity.Id, DownloadTablename = "fb_pa_goods_gl" });

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

        [Transaction]
        public string GenarateId(string gsCode)
        {
            var setting = FbPaBaseSetService.Get();
            var allGbs = (from l in EntityRepository.LinqQuery select l).ToList();
            int maxId = allGbs.Select(p => Convert.ToInt32(p.Id.Right(setting.GoodsGlLen.ToInt32()))).OrderByDescending(p => p).FirstOrDefault();
            if (maxId <= 0)
            {
                maxId = 0;
            }
            return gsCode + (maxId + 1).ToString().FillByStrings('0', setting.GoodsGbLen.ToInt32());
        }
    }
}
