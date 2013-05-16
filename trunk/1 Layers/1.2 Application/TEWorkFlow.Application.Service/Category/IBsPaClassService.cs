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
    public interface IBsPaClassService
    {
        string Create(BsPaClass entity);

        BsPaClass GetById(string id);

        void Save(BsPaClass entity);

        IList<BsPaClass> GetAll();

        void Update(BsPaClass entity);

        /// <summary>
        /// 删除指定BsPaClass
        /// </summary>
        /// <param name="entity"></param>
        void Delete(BsPaClass entity);

        /// <summary>
        /// BsPaClass
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<BsPaClass> entitys);


        SearchResult<BsPaClass> Search(SearchDtoBase<BsPaClass> c);

        void Delete(IList<string> ids);
    }
}