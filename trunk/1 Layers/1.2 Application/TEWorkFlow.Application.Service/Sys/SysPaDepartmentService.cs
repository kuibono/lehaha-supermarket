using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Application.Service.Sys
{
    public class SysPaDepartmentService : ISysPaDepartmentService
    {

        public IRepositoryGUID<SysPaDepartment> EntityRepository { get; set; }

        [Transaction]
        public string Create(SysPaDepartment entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public SysPaDepartment GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<SysPaDepartment> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(SysPaDepartment entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(SysPaDepartment entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<SysPaDepartment> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<SysPaDepartment> Search(SearchDtoBase<SysPaDepartment> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.dName) == false)
                {
                    q = q.Where(p => p.dName.Contains(c.entity.dName));
                }
                if (string.IsNullOrEmpty(c.entity.Higher) == false)
                {
                    q = q.Where(p => p.Higher.Contains(c.entity.Higher));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<SysPaDepartment> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.dName.Contains(key)
                    || l.Higher.Contains(key)
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
        [Transaction]
        public IList<SysTreeNodeDto> ModelListToDto(IList<SysPaDepartment> entitys)
        {
            IList<SysTreeNodeDto> result = new List<SysTreeNodeDto>();
            foreach (var entity in entitys)
            {
                result.Add(new SysTreeNodeDto()
                {
                    id = entity.Id,
                    pid = entity.Higher,
                    text = entity.dName
                });
            }
            return result;
        }
    }
}
