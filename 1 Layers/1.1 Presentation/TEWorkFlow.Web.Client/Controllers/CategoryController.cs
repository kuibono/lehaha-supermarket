using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Category;
using TEWorkFlow.Application.Service.Sys;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Application.Service.Archives;

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
