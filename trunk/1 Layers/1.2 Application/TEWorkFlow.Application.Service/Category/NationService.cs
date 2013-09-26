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
    public class NationService : INationService
    {
        public IRepositoryGUID<Nation> NationRepository { get; set; }

        [Transaction]
        public string Create(Nation entity)
        {
            return NationRepository.Save(entity);
        }

        [Transaction]
        public Nation GetById(string id)
        {
            return NationRepository.Get(id);
        }

        [Transaction]
        public IList<Nation> GetAll()
        {
            return NationRepository.LinqQuery.ToList();
        }


        [Transaction]
        public void Save(Nation entity)
        {
            bool add = false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                if (NationRepository.LinqQuery.Count(p => p.Id == entity.Id) > 0)
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
                NationRepository.Save(entity);
            }
            else
            {
                NationRepository.Update(entity);
            }
        }

        [Transaction]
        public void Update(Nation entity)
        {
            NationRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Nation entity)
        {
            NationRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Nation> entitys)
        {
            foreach (var entity in entitys)
            {
                NationRepository.Delete(entity);
            }
        }


        [Transaction]
        public IList<Nation> GetList(ISpecification<Nation> specification, Action<Orderable<Nation>> order)
        {
            return NationRepository.GetList(specification, order);
        }

        [Transaction]
        public SearchResult<Nation> Search(SearchDtoBase<Nation> c)
        {
            var q = NationRepository.LinqQuery;

            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            return q.ToList().ToSearchResult(count);
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = NationRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (Nation Nation in q)
            {
                Delete(Nation);
            }
        }
    }
}
