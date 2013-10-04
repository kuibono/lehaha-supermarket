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
    public class BranchPostService : IBranchPostService
    {

        public IRepositoryGUID<BranchPost> EntityRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> FbSupplierArchivesRepository { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }
        [Transaction]
        public string Create(BranchPost entity)
        {
            var allSuppliers = FbSupplierArchivesRepository.LinqQuery.ToList();
            string id=EntityRepository.Save(entity);
            TfDataDownloadService.AddDownload("sys_branch_Post", id);
            return id;
        }

        [Transaction]
        public BranchPost GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<BranchPost> GetAll(string supCode)
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }

        [Transaction]
        public void Update(BranchPost entity)
        {
            try
            {
                TfDataDownloadService.AddDownload("sys_branch_Post", entity.Id);
            }
            catch { }
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(BranchPost entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<BranchPost> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<BranchPost> Search(SearchDtoBase<BranchPost> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.Id) == false)
                {
                    q = q.Where(p => p.Id.Contains(c.entity.Id));
                }
                if (string.IsNullOrEmpty(c.entity.Title) == false)
                {
                    q = q.Where(p => p.Title.Contains(c.entity.Title));
                }
                if (string.IsNullOrEmpty(c.entity.PostUser) == false)
                {
                    q = q.Where(p => p.PostUser.Contains(c.entity.PostUser));
                }
                if (string.IsNullOrEmpty(c.entity.PostContent) == false)
                {
                    q = q.Where(p => p.PostContent.Contains(c.entity.PostContent));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<BranchPost> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.Id.Contains(key)
                    || l.Title.Contains(key)
                    || l.PostUser.Contains(key)
                    || l.PostContent.Contains(key)
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
