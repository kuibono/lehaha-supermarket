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
    [UserAuthorizeAttribute]
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

        public IEmemployeearchiveService EmemployeearchiveService { get; set; }
        public IFbSupplierArchivesService FbSupplierArchivesService { get; set; }
        #region SupType
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
        #endregion

        #region Area
        public ActionResult BsPaAreaList()
        {
            return View();
        }

        public JsonResult GetAreaList()
        {
            return Json(BsPaAreaService.Search(new Dto.SearchDtoBase<BsPaArea>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveArea()
        {
            BsPaArea e = new BsPaArea();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            e.Id = row["Id"] == null ? "" : row["Id"].ToString();
            e.AreaName = row["AreaName"].ToString();
            BsPaAreaService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteArea(string Id)
        {
            BsPaAreaService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Politics
        public ActionResult EmPaPoliticsList()
        {
            return View();
        }

        public JsonResult GetPoliticsList()
        {
            return Json(EmPaPoliticsService.Search(new Dto.SearchDtoBase<EmPaPolitics>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePolitics()
        {
            EmPaPolitics e = new EmPaPolitics();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            e.Id = row["Id"].ToString();
            e.Name = row["Name"].ToString();
            EmPaPoliticsService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePolitics(string Id)
        {
            EmPaPoliticsService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Nation
        public ActionResult NationList()
        {
            return View();
        }

        public JsonResult GetNationList()
        {
            return Json(NationService.Search(new Dto.SearchDtoBase<Nation>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNation()
        {
            Nation e = new Nation();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            e.Id = row["Id"].ToString();
            e.Name = row["Name"].ToString();
            NationService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteNation(string Id)
        {
            NationService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Class
        public ActionResult ClassList()
        {
            return View();
        }

        public JsonResult GetGoodsGbList()
        {
            return Json(FbPaGoodsGbService.Search(new Dto.SearchDtoBase<FbPaGoodsGb>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsGb()
        {
            FbPaGoodsGb e = new FbPaGoodsGb();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            if (row["Id"] != null)
            {
                e.Id = row["Id"].ToString();
            }
            if (string.IsNullOrEmpty(e.Id))
            {
                e.Id = null;
            }
            e.GbName = row["GbName"].ToString();
            FbPaGoodsGbService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGoodsGb(string Id)
        {
            FbPaGoodsGbService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetGoodsGmList()
        {
            return Json(FbPaGoodsGmService.Search(new Dto.SearchDtoBase<FbPaGoodsGm>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsGm()
        {
            FbPaGoodsGm e = new FbPaGoodsGm();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            if (row["Id"] != null)
            {
                e.Id = row["Id"].ToString();
            }
            if (string.IsNullOrEmpty(e.Id))
            {
                e.Id = "";
            }
            e.GmName = row["GmName"].ToString();
            e.GbCode = row["GbCode"].ToString();
            FbPaGoodsGmService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGoodsGm(string Id)
        {
            FbPaGoodsGmService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetGoodsGsList()
        {
            return Json(FbPaGoodsGsService.Search(new Dto.SearchDtoBase<FbPaGoodsGs>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsGs()
        {
            FbPaGoodsGs e = new FbPaGoodsGs();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            if (row["Id"] != null)
            {
                e.Id = row["Id"].ToString();
            }
            if (string.IsNullOrEmpty(e.Id))
            {
                e.Id = null;
            }
            e.GsName = row["GsName"].ToString();
            e.GmCode = row["GmCode"].ToString();
            FbPaGoodsGsService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGoodsGs(string Id)
        {
            FbPaGoodsGsService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetGoodsGlList()
        {
            return Json(FbPaGoodsGlService.Search(new Dto.SearchDtoBase<FbPaGoodsGl>()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGoodsGl()
        {
            FbPaGoodsGl e = new FbPaGoodsGl();
            Hashtable row = (Hashtable)(Request["data"].JsonDecode());
            if (row["Id"] != null)
            {
                e.Id = row["Id"].ToString();
            }
            if (string.IsNullOrEmpty(e.Id))
            {
                e.Id = null;
            }
            e.GlName = row["GlName"].ToString();
            e.GsCode = row["GsCode"].ToString();
            FbPaGoodsGlService.Save(e);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGoodsGl(string Id)
        {
            FbPaGoodsGlService.Delete(new List<string> { Id });
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        #endregion

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
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "1", Name = "共产党员" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "2", Name = "共青团员" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "3", Name = "民主党派" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "4", Name = "无党派民主人士" });
            EmPaPoliticsService.Create(new EmPaPolitics() { Id = "5", Name = "群众" });
            return null;
        }


        public ActionResult IniNations()
        {
            NationService.Create(new Nation() { Id = "1", Name = "汉" });
            NationService.Create(new Nation() { Id = "2", Name = "蒙古" });
            NationService.Create(new Nation() { Id = "3", Name = "朝鲜" });
            NationService.Create(new Nation() { Id = "4", Name = "回" });
            NationService.Create(new Nation() { Id = "5", Name = "满" });
            NationService.Create(new Nation() { Id = "6", Name = "壮" });
            return null;
        }
        public ActionResult IniPaSupTypes()
        {
            FbPaSupTypeService.Create(new FbPaSupType() { Id = "1", SupTypeName = "制造业" });
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
        /// 是否审核
        /// </summary>
        /// <returns></returns>
        public JsonResult ExameItems()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "", text = "--是否审核--" });
            items.Add(new item() { id = "1", text = "审核" });
            items.Add(new item() { id = "0", text = "未审核" });
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

        public JsonResult PrefixType()
        {
            List<item> items = new List<item>();
            items.Add(new item() { id = "大类", text = "大类" });
            items.Add(new item() { id = "中类", text = "中类" });
            items.Add(new item() { id = "小类", text = "小类" });
            items.Add(new item() { id = "细类", text = "细类" });
            items.Add(new item() { id = "无", text = "无" });
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
            var result = FbPaSupTypeService.GetAll().Select(p => new { id = p.Id, text = p.SupTypeName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllUsers()
        {
            var result = EmemployeearchiveService.GetAll().Select(p => new { id = p.Id, text = p.Emname }).ToList();
            //var result = SysLoginPowerService.GetAll().Select(p => new { id = p.Id, text = p.Emname }).ToList();
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
