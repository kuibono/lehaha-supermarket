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
    public interface IPcReturnDetailService
    {
        string Create(PcReturnDetail entity);

        PcReturnDetail GetById(string id);

        IList<PcReturnDetail> GetAll();

        IList<PcReturnDetail> GetDetailsByManageId(string manageId);

        void Update(PcReturnDetail entity);

        /// <summary>
        /// 删除指定PcReturnDetail
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcReturnDetail entity);

        /// <summary>
        /// PcReturnDetail
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcReturnDetail> entitys);


        SearchResult<PcReturnDetail> Search(SearchDtoBase<PcReturnDetail> c);

        IList<PcReturnDetail> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}