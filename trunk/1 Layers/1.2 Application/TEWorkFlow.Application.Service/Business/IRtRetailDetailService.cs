using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Application.Service.Business
{
    public interface IRtRetailDetailService
    {
        string Create(RtRetailDetail entity);

        RtRetailDetail GetById(string id);

        IList<RtRetailDetail> GetAll();

        void Update(RtRetailDetail entity);

        /// <summary>
        /// 删除指定RtRetailDetail
        /// </summary>
        /// <param name="entity"></param>
        void Delete(RtRetailDetail entity);

        /// <summary>
        /// RtRetailDetail
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<RtRetailDetail> entitys);


        SearchResult<RtRetailDetail> Search(SearchDtoBase<RtRetailDetail> c);

        IList<RtRetailDetail> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        IList<RtRetailDetail> GetDetailsByManageId(string manageId);
    }
}
