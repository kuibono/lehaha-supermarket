using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Web.Client.Common;
using TEWorkFlow.Application.Service.Report;
using TEWorkFlow.Domain;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public IReportSevice ReportSevice { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SupplierPurchase()
        {
            return View();
        }
        public ActionResult SupplierPurchaseHistory()
        {
            return View();
        }
        public ActionResult BranchPurchase()
        {
            return View();
        }
        public ActionResult BranchPurchaseHistory()
        {
            return View();
        }
        public ActionResult BranchRetail()
        {
            return View();
        }
        public ActionResult BranchPurchaseRetailCompare()
        {
            return View();
        }
        
    }
}
