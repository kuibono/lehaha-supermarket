using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Business;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;
using TEWorkFlow.Application.Service.Archives;
using TEWorkFlow.Web.Client.Common;
using System.Collections;
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
        public IPcPurchaseDetailService PcPurchaseDetailService { get; set; }
        public IGoodsArchivesService GoodsArchivesService { get; set; }
        public ActionResult PurchaseEdit(string id)
        {
            PcPurchaseManage model = new PcPurchaseManage();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SearchPurchaseList(SearchDtoBase<PcPurchaseManage> c, PcPurchaseManage s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(PcPurchaseManageService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            return Json(PcPurchaseManageService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseDetailList(string Id)
        {
            return Json(PcPurchaseDetailService.GetDetailsByManageId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsToPurchase(string Id)
        {

            //ArrayList datas = (ArrayList)(Request["data"].JsonDecode());
            Hashtable row = (Hashtable)(Request["data"].JsonDecode()); // (Hashtable)datas[0];
            bool IsAdd = row["_state"].ToString() == "added";

            PcPurchaseDetail detail = new PcPurchaseDetail();
            if (!IsAdd)
            {
                detail = PcPurchaseDetailService.GetById(row["Id"].ToString());
            }

            GoodsArchives good = GoodsArchivesService.GetById(row["GoodsCode"].ToString());
            
            detail.GoodsCode = good.Id;
            detail.GoodsBarCode = good.GoodsBarCode;
            detail.Specification = good.Specification;
            detail.PackUnitCode = good.PackUnitCode;
            detail.OfferMin = good.OfferMin;
            detail.PackCoef = good.PackCoef;
            detail.SalePrice = good.SalePrice;
            detail.PurchasePrice = good.PurchasePrice;
            detail.NontaxPurchasePrice = good.NontaxPurchasePrice;

            detail.PurchaseQty = Convert.ToInt32(row["PurchaseQty"]);
            detail.StockQty = Convert.ToInt32(row["StockQty"]);
            detail.OrderQty = Convert.ToInt32(row["OrderQty"]);
            detail.PackQty = Convert.ToInt32(row["PackQty"]);
            detail.PutinQty = Convert.ToInt32(row["PutinQty"]);
            detail.ProduceDate = row["ProduceDate"].ToNullAbleDateTime();

            //计算金额
            detail.PurchaseMoney = detail.PurchaseQty * detail.SalePrice;
            detail.NontaxPurchaseMoney = detail.PurchaseQty * detail.NontaxPurchasePrice;
            
            if (IsAdd)
            {
                detail.ManageId = Id;
                detail.Id = Guid.NewGuid().ToString();
                PcPurchaseDetailService.Create(detail);
            }
            else
            {
                PcPurchaseDetailService.Update(detail);
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PurchaseDelete(List<string> ids)
        {
            PcPurchaseManageService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePurchaseDetail(string Id)
        {
            PcPurchaseDetailService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePurchase(PcPurchaseManage s, PcPurchaseDetail Detail)
        {
            if (s.HaveId)
            {
                PcPurchaseManageService.Update(s);
            }
            else
            {
                s.Id = Guid.NewGuid().ToString();
                PcPurchaseManageService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
