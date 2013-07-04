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
    public interface IPcPurchaseManageHistoryService
    {
        string Create(PcPurchaseManageHistory entity);

        void Save(PcPurchaseManageHistory entity);

        PcPurchaseManageHistory GetById(string id);

        IList<PcPurchaseManageHistory> GetAll();

        void Update(PcPurchaseManageHistory entity);

        /// <summary>
        /// 删除指定PcPurchaseManageHistory
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcPurchaseManageHistory entity);

        /// <summary>
        /// PcPurchaseManageHistory
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcPurchaseManageHistory> entitys);

        void UpdatePurchaseAmount(string id);

        SearchResult<PcPurchaseManageHistory> Search(string SupplierId, SearchDtoBase<PcPurchaseManageHistory> c);

        SearchResult<PcPurchaseManageHistory> Search(DateTime? dateS, DateTime? dateE, string Encode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PcPurchaseManageHistory> SearchReportByBranch(DateTime? dateS, DateTime? dateE, string BranchId, int pageSize = 20, int pageIndex = 1);

        SearchResult<PcPurchaseManageHistory> SearchReportBySupplier(DateTime? dateS, DateTime? dateE, string SupCode, string bCode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PurchaseGoodsResult> SearchForPurchaseGoods(DateTime? dateS, DateTime? dateE, string BranchId,string SupCode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PurchaseSupplierResult> SearchForPurchaseSupllier(DateTime? dateS, DateTime? dateE, string Encode, int pageSize = 20, int pageIndex = 1);

        IList<PcPurchaseManageHistory> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        bool IsGoodsHavePurchase(string goodCode);

        bool IsSupplierHavePurchase(string supCode);

        bool IsBranchHavePurchase(string bCode);
    }
}