using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Category
{
    public interface INationService
    {
        string Create(Nation entity);

        Nation GetById(string id);

        IList<Nation> GetAll();

        void Save(Nation entity);

        void Update(Nation entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Nation entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Nation> entitys);


        IList<Nation> GetList(ISpecification<Nation> specification, Action<Orderable<Nation>> order);

        SearchResult<Nation> Search(SearchDtoBase<Nation> c);

        void Delete(IList<string> ids);
    }
}
