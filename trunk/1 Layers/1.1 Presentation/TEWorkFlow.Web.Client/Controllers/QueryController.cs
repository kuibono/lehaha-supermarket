using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Domain.Business;
using TEWorkFlow.Application.Service.Business;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class QueryController : Controller
    {
        //
        // GET: /Query/

        public IPcPurchaseManageService PcPurchaseManageService { get; set; }
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

        public ActionResult BranchPurchaseOrder()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseOrder(string branch, DateTime? dates, DateTime? datee)
        {
            return Json(PcPurchaseManageService.SearchReportByBranch(dates, datee, branch), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchPurchaseGoods()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseGoods(string branch, DateTime? dates, DateTime? datee)
        {
            return Json(PcPurchaseManageService.SearchForPurchaseGoods(dates, datee, branch), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchPurchaseSupplier()
        {
            return View();
        }

        public JsonResult SearchBranchPurchaseSupplier(string branch, DateTime? dates, DateTime? datee)
        {
            return Json(PcPurchaseManageService.SearchForPurchaseSupllier(dates, datee, branch), JsonRequestBehavior.AllowGet);
        }
    }
}
