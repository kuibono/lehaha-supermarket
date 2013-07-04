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
    public interface IPcPurchaseDetailHistoryService
    {
        string Create(PcPurchaseDetailHistory entity);

        PcPurchaseDetailHistory GetById(string id);

        IList<PcPurchaseDetailHistory> GetAll();



        void Update(PcPurchaseDetailHistory entity);

        /// <summary>
        /// 删除指定PcPurchaseDetailHistory
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcPurchaseDetailHistory entity);

        /// <summary>
        /// PcPurchaseDetailHistory
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcPurchaseDetailHistory> entitys);

        IList<PcPurchaseDetailHistory> GetDetailsByManageId(string manageId);

        SearchResult<PcPurchaseDetailHistory> Search(SearchDtoBase<PcPurchaseDetailHistory> c);

        void Delete(IList<string> ids);
    }
}