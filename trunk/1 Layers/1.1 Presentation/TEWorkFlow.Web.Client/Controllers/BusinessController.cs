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

        public IGoodsArchivesService GoodsArchivesService { get; set; }

        #region 采购
        public IPcPurchaseManageService PcPurchaseManageService { get; set; }
        public IPcPurchaseDetailService PcPurchaseDetailService { get; set; }
       

        public ActionResult PurchaseList()
        {
            return View();
        }
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

        public JsonResult SearchAllPurchaseList(string key)
        {
            return Json(PcPurchaseManageService.Search(key), JsonRequestBehavior.AllowGet);
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

            //如果订单不存在，则现在新建一个
            if (PcPurchaseManageService.GetById(Id)==null || PcPurchaseManageService.GetById(Id).HaveId == false)
            {
                //不存在
                PcPurchaseManage manage = new PcPurchaseManage();
                manage.Id = Id;
                PcPurchaseManageService.Create(manage);
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
            //if (s.HaveId)
            //{
            //    PcPurchaseManageService.Update(s);
            //}
            //else
            //{
            //    s.Id = Guid.NewGuid().ToString();
            //    PcPurchaseManageService.Create(s);
            //}
            PcPurchaseManageService.Save(s);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion 

        #region 退货
        public IPcReturnManageService PcReturnManageService { get; set; }
        public IPcReturnDetailService PcReturnDetailService { get; set; }

        public ActionResult ReturnList()
        {
            return View();
        }

        public ActionResult ReturnEdit(string id)
        {
            PcReturnManage model = new PcReturnManage();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcReturnManageService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SearchReturnList(SearchDtoBase<PcReturnManage> c, PcReturnManage s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(PcReturnManageService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            return Json(PcReturnManageService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReturnDetailList(string Id)
        {
            return Json(PcReturnDetailService.GetDetailsByManageId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsToReturn(string Id)
        {

            //ArrayList datas = (ArrayList)(Request["data"].JsonDecode());
            Hashtable row = (Hashtable)(Request["data"].JsonDecode()); // (Hashtable)datas[0];
            bool IsAdd = row["_state"].ToString() == "added";

            PcReturnDetail detail = new PcReturnDetail();
            if (!IsAdd)
            {
                detail = PcReturnDetailService.GetById(row["Id"].ToString());
            }

            GoodsArchives good = GoodsArchivesService.GetById(row["GoodsCode"].ToString());

            detail.GoodsCode = good.Id;
            detail.GoodsBarCode = good.GoodsBarCode;
            detail.Specification = good.Specification;
            detail.PackUnitCode = good.PackUnitCode;
            detail.PackCoef = good.PackCoef;
            detail.SalePrice = good.SalePrice;
            detail.PurchasePrice = good.PurchasePrice;
            detail.NontaxPurchasePrice = good.PurchasePrice;
            





            detail.ReturnQty = Convert.ToInt32(row["ReturnQty"]);
            detail.PackQty = Convert.ToInt32(row["PackQty"]);

            //计算金额
            detail.ReturnMoney = detail.ReturnQty * detail.PurchasePrice;
            detail.NontaxReturnMoney = detail.ReturnQty * detail.NontaxPurchasePrice;

            if (IsAdd)
            {
                detail.RtNumber = Id;
                detail.Id = Guid.NewGuid().ToString();
                PcReturnDetailService.Create(detail);
            }
            else
            {
                PcReturnDetailService.Update(detail);
            }

            //如果订单不存在，则现在新建一个
            if (PcReturnManageService.GetById(Id)==null || PcReturnManageService.GetById(Id).HaveId == false)
            {
                //不存在
                PcReturnManage manage = new PcReturnManage();
                manage.Id = Id;
                PcReturnManageService.Create(manage);
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReturnDelete(List<string> ids)
        {
            PcReturnManageService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteReturnDetail(string Id)
        {
            PcReturnDetailService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveReturn(PcReturnManage s)
        {
            PcReturnManageService.Save(s);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 补货
        public IPcSupplementManageService PcSupplementManageService { get; set; }
        public IPcSupplementDetailService PcSupplementDetailService { get; set; }


        public ActionResult SupplementList()
        {
            return View();
        }
        public ActionResult SupplementEdit(string id)
        {
            PcSupplementManage model = new PcSupplementManage();
            model.Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcSupplementManageService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SearchSupplementList(SearchDtoBase<PcSupplementManage> c, PcSupplementManage s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(PcSupplementManageService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            return Json(PcSupplementManageService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllSupplementList(string key)
        {
            return Json(PcSupplementManageService.Search(key), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupplementDetailList(string Id)
        {
            return Json(PcSupplementDetailService.GetDetailsByManageId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsToSupplement(string Id)
        {

            //ArrayList datas = (ArrayList)(Request["data"].JsonDecode());
            Hashtable row = (Hashtable)(Request["data"].JsonDecode()); // (Hashtable)datas[0];
            bool IsAdd = row["_state"].ToString() == "added";

            PcSupplementDetail detail = new PcSupplementDetail();
            if (!IsAdd)
            {
                detail = PcSupplementDetailService.GetById(row["Id"].ToString());
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
                PcSupplementDetailService.Create(detail);
            }
            else
            {
                PcSupplementDetailService.Update(detail);
            }
            //如果订单不存在，则现在新建一个
            if (PcSupplementManageService.GetById(Id)==null || PcSupplementManageService.GetById(Id).HaveId == false)
            {
                //不存在
                PcSupplementManage manage = new PcSupplementManage();
                manage.Id = Id;
                PcSupplementManageService.Create(manage);
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SupplementDelete(List<string> ids)
        {
            PcSupplementManageService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSupplementDetail(string Id)
        {
            PcSupplementDetailService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSupplement(PcSupplementManage s, PcSupplementDetail Detail)
        {
            PcSupplementManageService.Save(s);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }
}
