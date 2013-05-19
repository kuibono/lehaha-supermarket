using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Category
{
    public interface IEmPaPoliticsService
    {
        string Create(EmPaPolitics entity);

        EmPaPolitics GetById(string id);

        IList<EmPaPolitics> GetAll();

        void Save(EmPaPolitics entity);

        void Update(EmPaPolitics entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(EmPaPolitics entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<EmPaPolitics> entitys);


        IList<EmPaPolitics> GetList(ISpecification<EmPaPolitics> specification, Action<Orderable<EmPaPolitics>> order);

        SearchResult<EmPaPolitics> Search(SearchDtoBase<EmPaPolitics> c);

        void Delete(IList<string> ids);
    }
}
