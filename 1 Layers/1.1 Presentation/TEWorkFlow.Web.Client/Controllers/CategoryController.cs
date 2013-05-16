using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Category;
using TEWorkFlow.Application.Service.Sys;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Application.Service.Archives;
using System.Collections;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public IEmPaPoliticsService EmPaPoliticsService { get; set; }
        public INationService NationService { get; set; }
        public IFbPaSupTypeService FbPaSupTypeService { get; set; }
        public ISysLoginPowerService SysLoginPowerService { get; set; }
        public IBsPaClassService BsPaClassService { get; set; }
        public IBsPaAreaService BsPaAreaService { get; set; }


        public ActionResult SupTypeList()
        {
            return View();
        }

        public JsonResult GetSupTypeList()
        {
            return Json(FbPaSupTypeService.Search(new Dto.SearchDtoBase<FbPaSupType>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSupType()
        {
            FbPaSupType e = new FbPaSupType();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            e.Id = row["Id"].ToString();
            e.SupTypeName = row["SupTypeName"].ToString();
            FbPaSupTypeService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSupType(string Id)
        {
            FbPaSupTypeService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        // ////////////////////////////////////////////////////////////////
        public ActionResult PaClassList()
        {
            return View();
        }

        public JsonResult GetPaClassList()
        {
            return Json(BsPaClassService.Search(new Dto.SearchDtoBase<BsPaClass>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePaClass()
        {
            BsPaClass e = new BsPaClass();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            e.Id = row["Id"] == null ? Guid.NewGuid().ToString() : row["Id"].ToString();
            e.ClassName = row["ClassName"].ToString();
            BsPaClassService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePaClass(string Id)
        {
            BsPaClassService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }



        #region  category

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IniPolitics()
        {
            EmPaPoliticsService.Create(new EmPaPolitics() {Id = "1", Name = "共产党员"});
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "2", Name = "共青团员" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "3", Name = "民主党派" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "4", Name = "无党派民主人士" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "5", Name = "群众" });
            return null;
        }

        public ActionResult IniNations()
        {
            NationService.Create(new Nation() {Id="1",Name = "汉"});
            NationService.Create(new Nation() { Id = "2", Name = "蒙古" });
            NationService.Create(new Nation() { Id = "3", Name = "朝鲜" });
            NationService.Create(new Nation() { Id = "4", Name = "回" });
            NationService.Create(new Nation() { Id = "5", Name = "满" });
            NationService.Create(new Nation() { Id = "6", Name = "壮" });
            return null;
        }
        public ActionResult IniPaSupTypes()
        {
            FbPaSupTypeService.Create(new FbPaSupType() {Id = "1", SupTypeName = "制造业"});
            FbPaSupTypeService.Create(new FbPaSupType() { Id = "2", SupTypeName = "食品副食" });
            FbPaSupTypeService.Create(new FbPaSupType() { Id = "3", SupTypeName = "服装" });
            FbPaSupTypeService.Create(new FbPaSupType() { Id = "4", SupTypeName = "烟酒" });
            return null;
        }

        public JsonResult Sex()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "0", text = "男" });
            items.Add(new item() { id = "1", text = "女" });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Marriage()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "0", text = "未婚" });
            items.Add(new item() { id = "1", text = "已婚" });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 采购方式
        /// </summary>
        /// <returns></returns>
        public JsonResult PcMode()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "传真", text = "传真" });
            items.Add(new item() { id = "Email", text = "Email" });
            items.Add(new item() { id = "电话", text = "电话" });
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 采购类型
        /// </summary>
        /// <returns></returns>
        public JsonResult PcType()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "普通订货", text = "普通订货" });
            items.Add(new item() { id = "开放订货", text = "开放订货" });
            items.Add(new item() { id = "促销订货", text = "促销订货" });
            items.Add(new item() { id = "赠品订货", text = "赠品订货" });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Politics()
        {
            var result = EmPaPoliticsService.GetAll().Select(p => new { id = p.Id, text = p.Name }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Nations()
        {
            var result = NationService.GetAll().Select(p => new { id = p.Id, text = p.Name }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaSupTypes()
        {
            var result=FbPaSupTypeService.GetAll().Select(p => new { id = p.Id, text = p.SupTypeName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllUsers()
        {
            var result = SysLoginPowerService.GetAll().Select(p => new { id = p.Id, text = p.Emname }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaClasses()
        {
            var result = BsPaClassService.GetAll().Select(p => new { id = p.Id, text = p.ClassName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaAreas()
        {
            var result = BsPaAreaService.GetAll().Select(p => new { id = p.Id, text = p.AreaName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 商品分类

        #region 大类
        public IFbPaGoodsGbService FbPaGoodsGbService { get; set; }
        public JsonResult GetFbPaGoodsGb()
        {
            var result = FbPaGoodsGbService.GetAll().Select(p => new { id = p.Id, text = p.GbName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 中类
        public IFbPaGoodsGmService FbPaGoodsGmService { get; set; }
        public JsonResult GetFbPaGoodsGm(string id)
        {
            var result = FbPaGoodsGmService.GetByGbId(id).Select(p => new { id = p.Id, text = p.GmName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 小类
        public IFbPaGoodsGsService FbPaGoodsGsService { get; set; }
        public JsonResult GetFbPaGoodsGs(string id)
        {
            var result = FbPaGoodsGsService.GetByGbId(id).Select(p => new { id = p.Id, text = p.GsName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 细类
        public IFbPaGoodsGlService FbPaGoodsGlService { get; set; }
        public JsonResult GetFbPaGoodsGl(string id)
        {
            var result = FbPaGoodsGlService.GetByGsId(id).Select(p => new { id = p.Id, text = p.GlName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }

    public class item
    {
        public string id { get; set; }

        public string text { get; set; }

    }
}
