using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Domain.Category;

namespace TEWorkFlow.Application.Service.Sys
{
    public class TfDataDownloadService : ITfDataDownloadService
    {

        public IRepositoryGUID<TfDataDownload> EntityRepository { get; set; }
        public IRepositoryGUID<BsBranchArchives> BranchRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> FbSupplierArchivesRepository { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsArchivesRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGb> GbRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGm> GmRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGs> GsRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGl> GlRepository { get; set; }
        public IRepositoryGUID<FbGoodsArchivesBar> FbGoodsArchivesBarRepository { get; set; }
        public IRepositoryGUID<FbGoodsArchivesSupplier> FbGoodsArchivesSupplierRepository { get; set; }
        public IRepositoryGUID<FbPaBaseSet> FbPaBaseSetRepository { get; set; }
        [Transaction]
        public void AddDownload(string table, string id)
        {
            var branchs = BranchRepository.LinqQuery.Where(p=>p.IfSend=="true").ToList();
            foreach (var branch in branchs)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = branch.Id;
                d.DownloadKeyvalue = id;
                d.DownloadTablename = table;
                EntityRepository.Save(d);
            }

            //EntityRepository.Save(new TfDataDownload
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    DownloadBranchcode = "0",
            //    DownloadKeyvalue = id,
            //    DownloadTablename = table
            //});
        }
        [Transaction]
        public void AddAllArchivesToBranch(string bCode)
        {
            //return;

            var branch = BranchRepository.Get(bCode);
            if (branch == null || branch.IfExamine == "0")
            {
                return;
            }

            TfDataDownload ds = new TfDataDownload();
            ds.Id = Guid.NewGuid().ToString();
            ds.DownloadBranchcode = bCode;
            ds.DownloadKeyvalue = bCode;
            ds.DownloadTablename = "bs_branch_archives";
            EntityRepository.Save(ds);


            string str_goods = string.Format("INSERT INTO tf_data_download  select newid() as download_id, 'fb_goods_archives' as download_tablename, '{0}' as download_branchcode,goods_code as download_keyvalue,null as download_order from fb_goods_archives where if_examine = '1'",bCode);
            GoodsArchivesRepository.Session.CreateSQLQuery(str_goods).ExecuteUpdate();

            //string str_goodsbar = string.Format("INSERT INTO tf_data_download  select newid() as download_id, 'fb_goods_bar_code' as download_tablename, '{0}' as download_branchcode,goods_code as download_keyvalue,null as download_order from fb_goods_bar_code", bCode);
            //GoodsArchivesRepository.Session.CreateSQLQuery(str_goodsbar).ExecuteUpdate();


            var allSuppliers = FbSupplierArchivesRepository.LinqQuery.Where(p => p.IfExamine == "1").ToList();
            foreach (var supplier in allSuppliers)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = supplier.Id;
                d.DownloadTablename = "fb_supplier_archives";
                EntityRepository.Save(d);
            }

            var allGb = GbRepository.LinqQuery.ToList();
            foreach (FbPaGoodsGb item in allGb)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = item.Id;
                d.DownloadTablename = "fb_pa_goods_gb";
                EntityRepository.Save(d);
            }

            var allGm = GmRepository.LinqQuery.ToList();
            foreach (FbPaGoodsGm item in allGm)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = item.Id;
                d.DownloadTablename = "fb_pa_goods_gm";
                EntityRepository.Save(d);
            }

            var allGs = GsRepository.LinqQuery.ToList();
            foreach (FbPaGoodsGs item in allGs)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = item.Id;
                d.DownloadTablename = "fb_pa_goods_gs";
                EntityRepository.Save(d);
            }

            var allGl = GlRepository.LinqQuery.ToList();
            foreach (FbPaGoodsGl item in allGl)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = item.Id;
                d.DownloadTablename = "fb_pa_goods_gl";
                EntityRepository.Save(d);
            }

            //var allGoodsBar = FbGoodsArchivesBarRepository.LinqQuery.ToList();
            //foreach (FbGoodsArchivesBar item in allGoodsBar)
            //{
            //    TfDataDownload d = new TfDataDownload();
            //    d.Id = Guid.NewGuid().ToString();
            //    d.DownloadBranchcode = bCode;
            //    d.DownloadKeyvalue = item.Id;
            //    d.DownloadTablename = "fb_goods_archives_bar";
            //    EntityRepository.Save(d);
            //}
            //string str_goodsBar = string.Format("INSERT INTO tf_data_download  select newid() as download_id, 'fb_goods_archives_bar' as download_tablename, '{0}' as download_branchcode,goods_code as download_keyvalue,null as download_order from fb_goods_archives_bar ", bCode);
            //GoodsArchivesRepository.Session.CreateSQLQuery(str_goodsBar).ExecuteUpdate();

            //var allGoodsSupplier = FbGoodsArchivesSupplierRepository.LinqQuery.ToList();
            //foreach (FbGoodsArchivesSupplier item in allGoodsSupplier)
            //{
            //    TfDataDownload d = new TfDataDownload();
            //    d.Id = Guid.NewGuid().ToString();
            //    d.DownloadBranchcode = bCode;
            //    d.DownloadKeyvalue = item.Id;
            //    d.DownloadTablename = "fb_goods_archives_supplier";
            //    EntityRepository.Save(d);
            //}
            //string str_gs = string.Format("INSERT INTO tf_data_download  select newid() as download_id, 'fb_goods_archives_supplier' as download_tablename, '{0}' as download_branchcode,goods_code as download_keyvalue,null as download_order from fb_goods_archives_supplier", bCode);
            //GoodsArchivesRepository.Session.CreateSQLQuery(str_gs).ExecuteUpdate();

            var allSets = FbPaBaseSetRepository.LinqQuery.ToList();
            foreach (FbPaBaseSet item in allSets)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = bCode;
                d.DownloadKeyvalue = item.GoodsPrefix;
                d.DownloadTablename = "fb_pa_base_set";
                EntityRepository.Save(d);
            }
        }

        [Transaction]
        public string Create(TfDataDownload entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public TfDataDownload GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<TfDataDownload> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(TfDataDownload entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(TfDataDownload entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<TfDataDownload> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<TfDataDownload> Search(SearchDtoBase<TfDataDownload> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.DownloadTablename) == false)
                {
                    q = q.Where(p => p.DownloadTablename.Contains(c.entity.DownloadTablename));
                }
                if (string.IsNullOrEmpty(c.entity.DownloadBranchcode) == false)
                {
                    q = q.Where(p => p.DownloadBranchcode.Contains(c.entity.DownloadBranchcode));
                }
                if (string.IsNullOrEmpty(c.entity.DownloadKeyvalue) == false)
                {
                    q = q.Where(p => p.DownloadKeyvalue.Contains(c.entity.DownloadKeyvalue));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<TfDataDownload> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.DownloadTablename.Contains(key)
                    || l.DownloadBranchcode.Contains(key)
                    || l.DownloadKeyvalue.Contains(key)
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