using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

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
        public ActionResult BranchRetail()
        {
            return View();
        }
    }
}
