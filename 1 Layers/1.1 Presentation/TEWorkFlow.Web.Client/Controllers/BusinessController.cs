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
using NSH.VSTO;
using System.EnterpriseServices;
namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class BusinessController : Controller
    {
        //
        // GET: /Business/

        public IGoodsArchivesService GoodsArchivesService { get; set; }

        #region 采购
        public IPcPurchaseManageService PcPurchaseManageService { get; set; }
        public IPcPurchaseDetailService PcPurchaseDetailService { get; set; }
        public IPcPurchaseDetailHistoryService PcPurchaseDetailHistoryService { get; set; }

        public ActionResult PurchaseList()
        {
            return View();
        }
        public ActionResult PurchaseEdit(string id)
        {
            PcPurchaseManage model = new PcPurchaseManage();
            model.PurchaseDate = DateTime.Now;
            model.ExpectArriveDate = DateTime.Now;
            model.CheckDate = DateTime.Now;
            model.CreateDate = DateTime.Now;
            model.ExamineDate = DateTime.Now;
            model.OperatorDate = DateTime.Now;
            if (Request["bCode"] != null)
            {
                string bcode = Request["bCode"].ToString();
                model.bCode = bcode;
            }

            var currentUser = Common.MyEnv.CurrentSupplier;
            model.EnCode = currentUser.Id;
            model.SupCode = currentUser.SupName;
            model.Id = _string.GenerateBillNumber();
            //if (Common.Env.IsSupplierLogin)
            //{
            //    var sup=Common.Env.CurrentSupplier;
            //    model.SupCode = sup.SupName;
            //    model.EnCode = sup.Id;
            //}

            if (string.IsNullOrEmpty(id) == false)
            {
                model = PcPurchaseManageService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SearchPurchaseList(SearchDtoBase<PcPurchaseManage> c, PcPurchaseManage s,DateTime? dates,DateTime? datee)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(PcPurchaseManageService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            return Json(PcPurchaseManageService.Search(Common.MyEnv.CurrentSupplier.Id,c,dates,datee), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllPurchaseList(string key)
        {
            return Json(PcPurchaseManageService.Search(key), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseDetailList(string Id)
        {
            return Json(PcPurchaseDetailService.GetDetailsByManageId(Id).OrderBy(p=>p.AddTime), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchaseDetailHistoryList(string Id)
        {
            return Json(PcPurchaseDetailHistoryService.GetDetailsByManageId(Id).OrderBy(p => p.AddTime), JsonRequestBehavior.AllowGet);
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
            detail.IsFree = Convert.ToBoolean(row["IsFree"]);
            detail.ProduceDate = row["ProduceDate"].ToNullAbleDateTime();

            //计算金额
            detail.PurchaseMoney = detail.PurchaseQty * detail.PurchasePrice;
            detail.NontaxPurchaseMoney = detail.PurchaseQty * detail.PurchasePrice;

            //如果选择赠品，则金额为零
            if (detail.IsFree)
            {
                detail.PurchaseMoney = 0;
            }
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
            PcPurchaseManage manage = PcPurchaseManageService.GetById(Id);

            if (manage == null || manage.HaveId == false)
            {
                //不存在
                manage = new PcPurchaseManage();
                manage.Id = Id;
                PcPurchaseManageService.Create(manage);
            }
            PcPurchaseManageService.UpdatePurchaseAmount(Id);
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

        public JsonResult SearchForReport(SearchDtoBase<PcPurchaseManage> c, DateTime? dateS, DateTime? dateE, string Encode)
        {
            return Json(PcPurchaseManageService.Search(dateS, dateE, Encode, c.pageSize, c.pageIndex), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Test()
        {
            return Json(PcPurchaseManageService.SearchReportByBranch(new DateTime(2013,1,1),new DateTime(2014,1,1),"2740c64d-8dcb-45ef-92cc-57ff24442184"),JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseExame(string id)
        {
            PcPurchaseManage entity = PcPurchaseManageService.GetById(id);
            if (entity.HaveId)
            {
                entity.IfExamine = "1";
                PcPurchaseManageService.Update(entity);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
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
            if (Request["bCode"] != null)
            {
                string bcode = Request["bCode"].ToString();
                model.bCode = bcode;
            }
            var currentUser = Common.MyEnv.CurrentSupplier;
            model.EnCode = currentUser.Id;
            model.SupCode = currentUser.SupName;

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
            return Json(PcReturnManageService.Search(Common.MyEnv.CurrentSupplier.Id,c), JsonRequestBehavior.AllowGet);
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
            if (PcReturnManageService.GetById(Id) == null || PcReturnManageService.GetById(Id).HaveId == false)
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
            if (Request["bCode"] != null)
            {
                string bcode = Request["bCode"].ToString();
                model.bCode = bcode;
            }
            model.Id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            model.CreateDate = DateTime.Now;
            model.Operator = Common.MyEnv.CurrentEmployee.Id;

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
            if (PcSupplementManageService.GetById(Id) == null || PcSupplementManageService.GetById(Id).HaveId == false)
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

        #region 销售
        public IRtRetailManageService RtRetailManageService { get; set; }
        public IRtRetailDetailService RtRetailDetailService { get; set; }

        public JsonResult GetRetailDetailList(string Id)
        {
            return Json(RtRetailDetailService.GetDetailsByManageId(Id).OrderBy(p => p.RtDate), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
