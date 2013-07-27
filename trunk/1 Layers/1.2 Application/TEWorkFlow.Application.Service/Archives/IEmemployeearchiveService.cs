using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Archives
{
    public interface IEmemployeearchiveService
    {
        string Create(Ememployeearchive entity);

        Ememployeearchive GetById(string id);

        IList<Ememployeearchive> GetAll();

        void Update(Ememployeearchive entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Ememployeearchive entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Ememployeearchive> entitys);


        IList<Ememployeearchive> GetList(ISpecification<Ememployeearchive> specification, Action<Orderable<Ememployeearchive>> order);

        SearchResult<Ememployeearchive> Search(SearchDtoBase<Ememployeearchive> c);

        void Delete(IList<string> ids);

        string GenerateId();
    }
}
