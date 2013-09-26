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

        void Save(PcPurchaseManage entity);

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

        void UpdatePurchaseAmount(string id);

        SearchResult<PcPurchaseManage> Search(string SupplierId, SearchDtoBase<PcPurchaseManage> c,DateTime? dates,DateTime? dateE);

        SearchResult<PcPurchaseManage> Search(DateTime? dateS, DateTime? dateE, string Encode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PcPurchaseManage> SearchReportByBranch(DateTime? dateS, DateTime? dateE, string BranchId, int pageSize = 20, int pageIndex = 1);

        SearchResult<PcPurchaseManage> SearchReportBySupplier(DateTime? dateS, DateTime? dateE, string SupCode, string bCode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PurchaseGoodsResult> SearchForPurchaseGoods(DateTime? dateS, DateTime? dateE, string BranchId,string SupCode, int pageSize = 20, int pageIndex = 1);

        SearchResult<PurchaseSupplierResult> SearchForPurchaseSupllier(DateTime? dateS, DateTime? dateE, string Encode, int pageSize = 20, int pageIndex = 1);

        IList<PcPurchaseManage> Search(string key, int pageSize = 20, int pageIndex = 1);

        //IList<PcPurchaseManage> Search(string key, int pageSize = 20, int pageIndex = 1,DateTime dateS=DateTime.Now.Date,DateTime dateE=DateTime.Now.AddDays(1).Date);

        void Delete(IList<string> ids);

        bool IsGoodsHavePurchase(string goodCode);

        bool IsSupplierHavePurchase(string supCode);

        bool IsBranchHavePurchase(string bCode);
    }
}