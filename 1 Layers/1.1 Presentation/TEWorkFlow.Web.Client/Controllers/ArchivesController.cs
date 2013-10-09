using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Archives;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;
using TEWorkFlow.Application.Service.Business;
using TEWorkFlow.Web.Client.Common;
using NSH.Core.Domain;
using NSH.VSTO;
using TEWorkFlow.Application.Service.Sys;
namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class ArchivesController : Controller
    {
        //
        // GET: /Archives/

        public IPcPurchaseManageService PcPurchaseManageService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region Ememployeearchive

        public IEmemployeearchiveService EmemployeearchiveService { get; set; }



        public ActionResult EmployeeList()
        {
            return View();
        }
        public ActionResult EmployeeEdit(string id)
        {
            Ememployeearchive em = new Ememployeearchive()
                                     {
                                         Birthday = DateTime.Now.AddYears(-20),
                                         Accededay = DateTime.Now,
                                         Graduateday = DateTime.Now
                                     };
            if (!string.IsNullOrEmpty(id))
            {
                em = EmemployeearchiveService.GetById(id);
            }
            return View(em);
        }
        public JsonResult SearchEmployeeArchiveList(SearchDtoBase<Ememployeearchive> c, Ememployeearchive s)
        {
            c.entity = s;
            return Json(EmemployeearchiveService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEmployeeArchive(Ememployeearchive em)
        {
            if (em.HaveId)
            {
                EmemployeearchiveService.Update(em);
            }
            else
            {
                em.Id = EmemployeearchiveService.GenerateId();
                EmemployeearchiveService.Create(em);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeeDelete(List<string> ids)
        {

            EmemployeearchiveService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 供应商

        private IFbSupplierArchivesService FbSupplierArchivesService { set; get; }
        public ActionResult SupplierList()
        {
            return View();
        }
        public ActionResult SupplierEdit(string id)
        {
            // string loginName=FbSupplierArchivesService.GenerateLoginName();

            FbSupplierArchives model = new FbSupplierArchives()
                                         {
                                             //Id=FbSupplierArchivesService.GenerateId(),
                                             CreateDate = DateTime.Now,
                                             ExamineDate = DateTime.Now,
                                             OperatorDate = DateTime.Now
                                         };

            if (string.IsNullOrEmpty(id) == false)
            {
                model = FbSupplierArchivesService.GetById(id);
            }
            return View(model);
        }

        public JsonResult SupplierExame(string id)
        {
            FbSupplierArchives entity = FbSupplierArchivesService.GetById(id);
            if (entity.HaveId)
            {
                entity.IfExamine = entity.IfExamine == "1" ? "0" : "1";
                FbSupplierArchivesService.Update(entity);
                return Json(entity.IfExamine == "1", JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierDelete(List<string> ids)
        {
            if (Request["confirm"] == null && PcPurchaseManageService.IsSupplierHavePurchase(ids.First()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            FbSupplierArchivesService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchSupplierArchiveList(SearchDtoBase<FbSupplierArchives> c, FbSupplierArchives s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(FbSupplierArchivesService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            return Json(FbSupplierArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllSuppliers(string id)
        {
            return Json(FbSupplierArchivesService.Search(id, 10000), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchAllSuppliersWithGoodsCount(string id)
        {
            return Json(FbSupplierArchivesService.SearchWithGoodsCount(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSupplierArchive(FbSupplierArchives s)
        {
            if (s.HaveId)
            {
                FbSupplierArchivesService.Update(s);
            }
            else
            {
                //s.Id = Guid.NewGuid().ToString();
                FbSupplierArchivesService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSupplierArchive(string key)
        {
            int pageSize = 20;
            if (Request["pageSize"] != null)
            {
                pageSize = Convert.ToInt32(Request["pageSize"]);
            }
            var result = FbSupplierArchivesService.Search(key, pageSize);
            if (Common.MyEnv.IsSupplierLogin)
            {
                var currentSupplier = Common.MyEnv.CurrentSupplier;
                result = new List<FbSupplierArchives>();
                result.Add(currentSupplier);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSupplierArchiveNoPager(string key)
        {
            int pageSize = 999999;
            if (Request["pageSize"] != null)
            {
                pageSize = Convert.ToInt32(Request["pageSize"]);
            }
            var result = FbSupplierArchivesService.Search(key, pageSize);
            if (Common.MyEnv.IsSupplierLogin)
            {
                var currentSupplier = Common.MyEnv.CurrentSupplier;
                result = new List<FbSupplierArchives>();
                result.Add(currentSupplier);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierSelectWindow()
        {
            return View();
        }

        #endregion


        #region 分店

        public IBsBranchArchivesService BsBranchArchivesService { get; set; }

        public ActionResult BranchList()
        {
            return View();
        }

        public JsonResult SearchBranchArchiveList(SearchDtoBase<BsBranchArchives> c, BsBranchArchives s)
        {
            if (c != null && s != null)
            {
                c.entity = s;
                if (Common.MyEnv.IsSupplierLogin)
                {
                    return Json(BsBranchArchivesService.Search(c, Common.MyEnv.CurrentSupplier.Id), JsonRequestBehavior.AllowGet);
                }
                return Json(BsBranchArchivesService.Search(c), JsonRequestBehavior.AllowGet);
            }
            if (Request["key"] == null || string.IsNullOrEmpty(Request["key"]) == false)
            {
                if (Common.MyEnv.IsSupplierLogin)
                {
                    return Json(BsBranchArchivesService.Search(Request["key"], Common.MyEnv.CurrentSupplier.Id), JsonRequestBehavior.AllowGet);
                }
                return Json(BsBranchArchivesService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }
            c.entity = s;
            if (Common.MyEnv.IsSupplierLogin)
            {
                return Json(BsBranchArchivesService.Search(c, Common.MyEnv.CurrentSupplier.Id), JsonRequestBehavior.AllowGet);
            }
            return Json(BsBranchArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllranches(string id)
        {
            return Json(BsBranchArchivesService.Search(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchAllranchesForTree(string id)
        {
            return Json(BsBranchArchivesService.Search(id,999999), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchEdit(string id)
        {
            BsBranchArchives entity = new BsBranchArchives();
            if (string.IsNullOrEmpty(id) == false)
            {
                entity = BsBranchArchivesService.GetById(id);
            }
            if (string.IsNullOrEmpty(entity.SyncPassword))
            {
                entity.SyncPassword = DateTime.Now.ToString("ssfff");
            }
            return View(entity);
        }
        public ActionResult BranchView(string id)
        {
            BsBranchArchives entity = new BsBranchArchives();
            if (string.IsNullOrEmpty(id) == false)
            {
                entity = BsBranchArchivesService.GetById(id);
            }
            return View(entity);
        }

        public JsonResult SaveBranchArchive(BsBranchArchives s)
        {
            bool Exist = false;

            if (string.IsNullOrEmpty(s.InputBCode) == false && BsBranchArchivesService.GetById(s.InputBCode) == null)
            {
                s.Id = s.InputBCode;
            }
            if (string.IsNullOrEmpty(s.Id))
            {
                s.Id = Guid.NewGuid().ToString();
            }
            BsBranchArchivesService.Update(s);
            //if(s.HaveId)
            //{
            //    BsBranchArchivesService.Update(s);
            //}
            //else
            //{
            //    s.Id = Guid.NewGuid().ToString();
            //    BsBranchArchivesService.Create(s);
            //}
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        ITfDataDownloadService TfDataDownloadService { get; set; }
        public JsonResult AddAllArchivesToBranch(string id)//id 是 supcode
        {
            TfDataDownloadService.AddAllArchivesToBranch(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchDelete(List<string> ids)
        {
            if (Request["confirm"] == null && PcPurchaseManageService.IsBranchHavePurchase(ids.First()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            BsBranchArchivesService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BranchExame(string id)
        {
            BsBranchArchives entity = BsBranchArchivesService.GetById(id);
            if (entity.HaveId)
            {
                entity.IfExamine = entity.IfExamine == "1" ? "0" : "1";
                BsBranchArchivesService.Update(entity);
                return Json(entity.IfExamine == "1", JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 商品档案

        public IGoodsArchivesService GoodsArchivesService { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsArchivesRepository { get; set; }
        public ActionResult GoodsList()
        {
            return View();
        }
        public ActionResult GoodsListTree()
        {
            return View();
        }

        public JsonResult GoodsExame(string id)
        {
            GoodsArchives entity = GoodsArchivesService.GetById(id);
            if (entity.HaveId)
            {
                entity.IfExamine = entity.IfExamine == "1" ? "0" : "1";
                GoodsArchivesService.Update(entity);
                return Json(entity.IfExamine == "1", JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchGoodsArchiveList(SearchDtoBase<GoodsArchives> c, GoodsArchives s)
        {
            //if (string.IsNullOrEmpty(Request["key"]) == false)
            //{
            //    return Json(GoodsArchivesService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            //}

            c.entity = s;
            if (Common.MyEnv.IsSupplierLogin)
            {
                c.entity.SupCode = Common.MyEnv.CurrentSupplier.Id;
            }
            return Json(GoodsArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGoodsArchiveAutoComplete(string id)
        {
            if (Common.MyEnv.IsSupplierLogin)
            {
                string supcode = Common.MyEnv.CurrentSupplier.Id;
                var allGoods = GoodsArchivesRepository.LinqQuery.Where(p => p.SupCode == supcode && (p.IfExamine == "1" || p.IfExamine == "true")).AsCache(supcode);
                string key = id;
                if (string.IsNullOrEmpty(key))
                {
                    key = Request["key"];
                }
                if (key == null)
                {
                    key = "";
                }

                var result = allGoods.Where(p => p.GoodsBarCode.Contains(key)
                    || p.PyCode.Contains(key)
                    || p.GoodsName.Contains(key)
                    ).Select(p => new
                    {
                        p.Id,
                        p.GoodsBarCode,
                        p.PyCode,
                        p.GoodsName,
                        displayText = p.GoodsName+" "+p.GoodsBarCode + "," + p.PyCode
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllGoodsArchive(string id)
        {
            SearchDtoBase<GoodsArchives> c = new SearchDtoBase<GoodsArchives>();
            c.pageSize = 5;
            c.pageIndex = 1;
            if (string.IsNullOrEmpty(id))
            {
                c.key = Request["key"];
            }
            if (Common.MyEnv.IsSupplierLogin)
            {
                if (c.entity == null)
                {
                    c.entity = new GoodsArchives();
                }
                c.entity.SupCode = Common.MyEnv.CurrentSupplier.Id;
                //result = result.Where(p => p.SupCode == Common.MyEnv.CurrentSupplier.Id).ToList();
            }

            var result = GoodsArchivesService.Search(c).data;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsEdit(string id)
        {
            GoodsArchives entity = new GoodsArchives();
            entity.IfNew = "1";
            entity.CreateDate = DateTime.Now;
            entity.Operator = Common.MyEnv.CurrentEmployee.Id;
            entity.Assessor = Common.MyEnv.CurrentEmployee.Id;
            entity.OperatorDate = DateTime.Now;
            if (Request["SupCode"] != null)
            {
                entity.SupCode = Request["SupCode"].ToString();
            }
            if (string.IsNullOrEmpty(id) == false)
            {
                entity = GoodsArchivesService.GetById(id);
            }
            return View(entity);
        }
        public JsonResult SaveGoodsArchive(GoodsArchives s)
        {
            Result result = new Result();

            if (s.HaveId)
            {
                result = GoodsArchivesService.Update(s);
            }
            else
            {
                s.GoodsSubCode = GoodsArchivesService.GenarateGbCode();
                s.Id = GoodsArchivesService.GenarateId(s);
                result = GoodsArchivesService.Create(s);
            }
            if (string.IsNullOrEmpty(s.SupCode) == false)
            {
                Cache.Clear(s.SupCode);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GoodsDelete(List<string> ids)
        {
            if (Request["confirm"] == null && PcPurchaseManageService.IsGoodsHavePurchase(ids.First()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            GoodsArchivesService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsSelectWindow()
        {
            return View();
        }
        #endregion


        #region 供货商分店关系管理

        public IFbSupplierBranchRelationService FbSupplierBranchRelationService { get; set; }

        public ActionResult SupplierBranchRelationManagement()
        {
            return View();
        }
        public ActionResult BranchSupplierRelationManagement()
        {
            return View();
        }
        public JsonResult GetRelationsBySupCode(string id)
        {
            return Json(FbSupplierBranchRelationService.GetAllRelationBySupplierCode(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRelationsByBCode(string id)
        {
            return Json(FbSupplierBranchRelationService.GetAllRelationByBranchCode(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetSupplierBranchRelationValue(string bCode, string supCode, bool value)
        {
            FbSupplierBranchRelationService.SetValue(bCode, supCode, value);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
