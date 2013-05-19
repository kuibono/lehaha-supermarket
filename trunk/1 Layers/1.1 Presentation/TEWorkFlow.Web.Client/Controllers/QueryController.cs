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
    }
}
