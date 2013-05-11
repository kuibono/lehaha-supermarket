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
    public interface IPcPurchaseManageService
    {
        string Create(PcPurchaseManage entity);

        PcPurchaseManage GetById(string id);

        IList<PcPurchaseManage> GetAll();

        void Update(PcPurchaseManage entity);

        /// <summary>
        /// 删除指定PcPurchaseManage
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcPurchaseManage entity);

        /// <summary>
        /// PcPurchaseManage
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcPurchaseManage> entitys);


        SearchResult<PcPurchaseManage> Search(SearchDtoBase<PcPurchaseManage> c);

        IList<PcPurchaseManage> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}