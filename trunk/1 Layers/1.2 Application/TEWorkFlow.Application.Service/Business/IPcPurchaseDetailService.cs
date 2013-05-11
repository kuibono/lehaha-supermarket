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
    public interface IPcPurchaseDetailService
    {
        string Create(PcPurchaseDetail entity);

        PcPurchaseDetail GetById(string id);

        IList<PcPurchaseDetail> GetAll();



        void Update(PcPurchaseDetail entity);

        /// <summary>
        /// 删除指定PcPurchaseDetail
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcPurchaseDetail entity);

        /// <summary>
        /// PcPurchaseDetail
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcPurchaseDetail> entitys);

        IList<PcPurchaseDetail> GetDetailsByManageId(string manageId);

        SearchResult<PcPurchaseDetail> Search(SearchDtoBase<PcPurchaseDetail> c);

        void Delete(IList<string> ids);
    }
}