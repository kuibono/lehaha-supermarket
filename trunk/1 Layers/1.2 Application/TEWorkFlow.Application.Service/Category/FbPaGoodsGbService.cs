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
using TEWorkFlow.Domain.Sys;
namespace TEWorkFlow.Application.Service.Category
{
    public class FbPaGoodsGbService : IFbPaGoodsGbService
    {

        public IRepositoryGUID<FbPaGoodsGb> EntityRepository { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }
        [Transaction]
        public string Create(FbPaGoodsGb entity)
        {
            entity.Id = GenarateId();
            string id = EntityRepository.Save(entity);
            TfDataDownloadService.AddDownload("fb_pa_goods_gb", id);
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = id, DownloadTablename = "fb_pa_goods_gb" });
            return id;
        }

        [Transaction]
        public FbPaGoodsGb GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbPaGoodsGb> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }
        [Transaction]
        public void Save(FbPaGoodsGb entity)
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
            TfDataDownloadService.AddDownload("fb_pa_goods_gb", entity.Id);
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = entity.Id, DownloadTablename = "fb_pa_goods_gb" });
        }


        [Transaction]
        public void Update(FbPaGoodsGb entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbPaGoodsGb entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbPaGoodsGb> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbPaGoodsGb> Search(SearchDtoBase<FbPaGoodsGb> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.GbName) == false)
                {
                    q = q.Where(p => p.GbName.Contains(c.entity.GbName));
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
        public string GenarateId()
        {
            var setting = FbPaBaseSetService.Get();
            var allGbs = (from l in EntityRepository.LinqQuery select l).ToList();
            int maxId = allGbs.Select(p => Convert.ToInt32(p.Id.Right(setting.GoodsGbLen.ToInt32()))).OrderByDescending(p => p).FirstOrDefault();
            if (maxId <= 0)
            {
                maxId = 0;
            }
            return (maxId + 1).ToString().FillByStrings('0', setting.GoodsGbLen.ToInt32());
        }
    }
}