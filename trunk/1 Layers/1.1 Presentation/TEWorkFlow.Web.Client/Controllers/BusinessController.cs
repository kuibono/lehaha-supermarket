using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Business;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class BusinessController : Controller
    {
        //
        // GET: /Business/

        public ActionResult PurchaseList()
        {
            return View();
        }
        public IPcPurchaseManageService PcPurchaseManageService { get; set; }

        public ActionResult PurchaseEdit(string id)
        {
            PcPurchaseManage model = new PcPurchaseManage();
            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SearchPurchaseList(SearchDtoBase<PcPurchaseManage> c, PcPurchaseManage s)
        {
            c.entity = s;
            return Json(PcPurchaseManageService.Search(c), JsonRequestBehavior.AllowGet);
        }
    }
}
