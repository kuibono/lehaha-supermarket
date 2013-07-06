using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Domain.Business;
using TEWorkFlow.Application.Service.Business;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Application.Service.Archives;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class QueryController : Controller
    {
        //
        // GET: /Query/

        public IPcPurchaseManageService PcPurchaseManageService { get; set; }
        public IGoodsArchivesService GoodsArchivesService { get; set; }
        public IRtRetailManageService RtRetailManageService{ get; set; }
        public IPcPurchaseManageHistoryService PcPurchaseManageHistoryService { get; set; }
        public ActionResult PurchaseQuery()
        {
            return View();
        }
        public ActionResult PurchaseView(string id)
        {
            PcPurchaseManage model = new PcPurchaseManage();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageService.GetById(id);
            }
            return View(model);
        }

        public ActionResult RetailView(string id)
        {
            PcPurchaseManage model = new PcPurchaseManage();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageService.GetById(id);
            }
            return View(model);
        }

        public ActionResult PurchaseHistoryView(string id)
        {
            PcPurchaseManageHistory model = new PcPurchaseManageHistory();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageHistoryService.GetById(id);
            }
            return View(model);
        }

        public ActionResult BranchPurchaseOrder()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseOrder(string branch, DateTime? dates, DateTime? datee)
        {
            return Json(PcPurchaseManageService.SearchReportByBranch(dates, datee, branch), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchSupplierOrder(string supCode,string bCode, DateTime? dates, DateTime? datee)
        {
            if (Common.MyEnv.IsSupplierLogin)
            {
                supCode = Common.MyEnv.CurrentSupplier.Id;
            }
            return Json(PcPurchaseManageService.SearchReportBySupplier(dates, datee, supCode,bCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchSupplierHistoryOrder(string supCode, string bCode, DateTime? dates, DateTime? datee)
        {
            if (Common.MyEnv.IsSupplierLogin)
            {
                supCode = Common.MyEnv.CurrentSupplier.Id;
            }
            return Json(PcPurchaseManageHistoryService.SearchReportBySupplier(dates, datee, supCode, bCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchBranchRetail(string bCode, DateTime? dates, DateTime? datee)
        {
            return Json(RtRetailManageService.SearchReportBySupplier(dates, datee, bCode), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult BranchPurchaseGoods()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseGoods(string branch,string SupCode, DateTime? dates, DateTime? datee)
        {
            if (Common.MyEnv.IsSupplierLogin)
            {
                SupCode = Common.MyEnv.CurrentSupplier.Id;
            }
            return Json(PcPurchaseManageService.SearchForPurchaseGoods(dates, datee, branch, SupCode), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchPurchaseSupplier()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseSupplier(string branch, DateTime? dates, DateTime? datee)
        {
            return Json(PcPurchaseManageService.SearchForPurchaseSupllier(dates, datee, branch), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsArchiveList()
        {
            return View();
        }

        public ActionResult ViewGoods(string id)
        {
            GoodsArchives entity = new GoodsArchives();
            if (string.IsNullOrEmpty(id) == false)
            {
                entity = GoodsArchivesService.GetById(id);
            }
            return View(entity);
        }
    }
}
