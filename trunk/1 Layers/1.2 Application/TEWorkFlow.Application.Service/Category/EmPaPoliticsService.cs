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
    public class EmPaPoliticsService:IEmPaPoliticsService
    {
        public IRepositoryGUID<EmPaPolitics> EmPaPoliticsRepository { get; set; }

        [Transaction]
        public string Create(EmPaPolitics entity)
        {
            return EmPaPoliticsRepository.Save(entity);
        }

        [Transaction]
        public EmPaPolitics GetById(string id)
        {
            return EmPaPoliticsRepository.Get(id);
        }

        [Transaction]
        public IList<EmPaPolitics> GetAll()
        {
            return EmPaPoliticsRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Save(EmPaPolitics entity)
        {
            bool add = false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                if (EmPaPoliticsRepository.LinqQuery.Count(p => p.Id == entity.Id) > 0)
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
                EmPaPoliticsRepository.Save(entity);
            }
            else
            {
                EmPaPoliticsRepository.Update(entity);
            }
        }

        [Transaction]
        public void Update(EmPaPolitics entity)
        {
            EmPaPoliticsRepository.Update(entity);
        }

        [Transaction]
        public void Delete(EmPaPolitics entity)
        {
            EmPaPoliticsRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<EmPaPolitics> entitys)
        {
            foreach (var entity in entitys)
            {
                EmPaPoliticsRepository.Delete(entity);
            }
        }


        [Transaction]
        public IList<EmPaPolitics> GetList(ISpecification<EmPaPolitics> specification, Action<Orderable<EmPaPolitics>> order)
        {
            return EmPaPoliticsRepository.GetList(specification, order);
        }

        [Transaction]
        public SearchResult<EmPaPolitics> Search(SearchDtoBase<EmPaPolitics> c)
        {
            var q = EmPaPoliticsRepository.LinqQuery;
            
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            return q.ToList().ToSearchResult(count);
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EmPaPoliticsRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (EmPaPolitics EmPaPolitics in q)
            {
                Delete(EmPaPolitics);
            }
        }
    }
}
