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
    public interface IFbPaSupTypeService
    {
        string Create(FbPaSupType entity);

        FbPaSupType GetById(string id);

        void Save(FbPaSupType entity);

        IList<FbPaSupType> GetAll();

        void Update(FbPaSupType entity);

        /// <summary>
        /// 删除指定FbPaSupType
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbPaSupType entity);

        /// <summary>
        /// FbPaSupType
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbPaSupType> entitys);


        SearchResult<FbPaSupType> Search(SearchDtoBase<FbPaSupType> c);

        void Delete(IList<string> ids);
    }
}
