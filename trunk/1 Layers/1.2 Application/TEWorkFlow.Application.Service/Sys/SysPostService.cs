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
    public class SysPostService : ISysPostService
    {

        public IRepositoryGUID<SysPost> EntityRepository { get; set; }
        public IRepositoryGUID<SysPostSupplierRelation> SysPostSupplierRelationRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> FbSupplierArchivesRepository { get; set; }

        [Transaction]
        public string Create(SysPost entity)
        {
            var allSuppliers = FbSupplierArchivesRepository.LinqQuery.ToList();
            string id=EntityRepository.Save(entity);

            foreach (FbSupplierArchives sup in allSuppliers)
            {
                SysPostSupplierRelation relation = new SysPostSupplierRelation();
                relation.Id = Guid.NewGuid().ToString();
                relation.PostId = id;
                relation.Readed = false;
                relation.ReadTime = new DateTime(2000, 1, 1);
                relation.SupCode = sup.Id;
                SysPostSupplierRelationRepository.Save(relation);
            }
            return id;
        }

        [Transaction]
        public SysPost GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<SysPost> GetAll(string supCode)
        {
            var result = EntityRepository.LinqQuery.ToList();

            if (string.IsNullOrEmpty(supCode)==false)
            {
                var allRelation = SysPostSupplierRelationRepository.LinqQuery.Where(p => p.SupCode == supCode).ToList();
                foreach (SysPost post in result)
                {
                    var relation = allRelation.Where(p => p.PostId == post.Id).FirstOrDefault();
                    if (relation != null)
                    {
                        post.Readed = relation.Readed;
                    }
                }
            }
            return result;
        }
        [Transaction]
        public void SetPostReaded(string id, string supCode)
        {
            SysPostSupplierRelation relation = SysPostSupplierRelationRepository.LinqQuery.Where(p => p.PostId == id && p.SupCode == supCode).FirstOrDefault();
            if (relation == null)
            {
                relation = new SysPostSupplierRelation();
                relation.Id = Guid.NewGuid().ToString();
                relation.PostId = id;
                relation.Readed = true;
                relation.ReadTime = DateTime.Now;
                relation.SupCode = supCode;
                SysPostSupplierRelationRepository.Save(relation);
            }
            else
            {
                relation.ReadTime = DateTime.Now;
                relation.Readed = true;
                SysPostSupplierRelationRepository.Update(relation);
            }

        }


        [Transaction]
        public void Update(SysPost entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(SysPost entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<SysPost> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<SysPost> Search(SearchDtoBase<SysPost> c)
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
        public IList<SysPost> Search(string key, int pageSize = 20, int pageIndex = 1)
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
