using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Category
{
    public class BsPaAreaService : IBsPaAreaService
    {

        public IRepositoryGUID<BsPaArea> EntityRepository { get; set; }

        [Transaction]
        public string Create(BsPaArea entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public BsPaArea GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<BsPaArea> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }

        [Transaction]
        public void Save(BsPaArea entity)
        {
            bool add = false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = GenerateId();
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
        public void Update(BsPaArea entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(BsPaArea entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<BsPaArea> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<BsPaArea> Search(SearchDtoBase<BsPaArea> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.AreaName) == false)
                {
                    q = q.Where(p => p.AreaName.Contains(c.entity.AreaName));
                }
                if (string.IsNullOrEmpty(c.entity.Functionary) == false)
                {
                    q = q.Where(p => p.Functionary.Contains(c.entity.Functionary));
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

        private string GenerateId()
        {
            var allItems = EntityRepository.LinqQuery.ToList();

            int id = 1;
            string str_id = id.ToString();
            while (allItems.Any(p => p.Id == str_id))
            {
                id++;
                str_id = id.ToString();
            }
            return str_id;
        }
    }
}