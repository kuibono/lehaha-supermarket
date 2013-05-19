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
    public interface IBsPaAreaService
    {
        string Create(BsPaArea entity);

        BsPaArea GetById(string id);

        IList<BsPaArea> GetAll();

        void Update(BsPaArea entity);

        void Save(BsPaArea entity);

        /// <summary>
        /// 删除指定BsPaArea
        /// </summary>
        /// <param name="entity"></param>
        void Delete(BsPaArea entity);

        /// <summary>
        /// BsPaArea
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<BsPaArea> entitys);


        SearchResult<BsPaArea> Search(SearchDtoBase<BsPaArea> c);

        void Delete(IList<string> ids);
    }
}