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

namespace TEWorkFlow.Application.Service.Sys
{
    public class TfDataDownloadService : ITfDataDownloadService
    {

        public IRepositoryGUID<TfDataDownload> EntityRepository { get; set; }
        public IRepositoryGUID<BsBranchArchives> BranchRepository { get; set; }
        [Transaction]
        public void AddDownload(string table, string id)
        {
            var branchs = BranchRepository.LinqQuery.ToList();
            foreach (var branch in branchs)
            {
                TfDataDownload d = new TfDataDownload();
                d.Id = Guid.NewGuid().ToString();
                d.DownloadBranchcode = branch.Id;
                d.DownloadKeyvalue = id;
                d.DownloadTablename = table;
                EntityRepository.Save(d);
            }

            EntityRepository.Save(new TfDataDownload
            {
                Id = Guid.NewGuid().ToString(),
                DownloadBranchcode = "0",
                DownloadKeyvalue = id,
                DownloadTablename = table
            });
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

            q = q.Skip((c.pageIndex - 1) * c.pageSize).Take(c.pageSize);
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