using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Archives;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class ArchivesController : Controller
    {
        //
        // GET: /Archives/

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
            Ememployeearchive em=new Ememployeearchive()
                                     {
                                         Birthday = DateTime.Now.AddYears(-20),
                                         Accededay = DateTime.Now,
                                         Graduateday = DateTime.Now
                                     };
            if(!string.IsNullOrEmpty(id))
            {
                em = EmemployeearchiveService.GetById(id);
            }
            return View(em);
        }
        public JsonResult SearchEmployeeArchiveList(SearchDtoBase<Ememployeearchive> c,Ememployeearchive s)
        {
            c.entity = s;
            return Json(EmemployeearchiveService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEmployeeArchive(Ememployeearchive em)
        {
            if(em.HaveId)
            {
                EmemployeearchiveService.Update(em);
            }
            else
            {
                em.Id = Guid.NewGuid().ToString();
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
            
            FbSupplierArchives model=new FbSupplierArchives()
                                         {
                                             //Id=Guid.NewGuid() .ToString(),
                                             CreateDate = DateTime.Now,
                                             ExamineDate = DateTime.Now,
                                             OperatorDate = DateTime.Now                                             
                                         };

            if(string.IsNullOrEmpty(id)==false)
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
                entity.IfExamine = entity.IfExamine=="true"?"false":"true";
                FbSupplierArchivesService.Update(entity);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierDelete(List<string> ids)
        {
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
            return Json(FbSupplierArchivesService.Search(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchAllSuppliersWithGoodsCount(string id)
        {
            return Json(FbSupplierArchivesService.SearchWithGoodsCount(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSupplierArchive(FbSupplierArchives s)
        {
            if(s.HaveId)
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
            var result = FbSupplierArchivesService.Search(key);
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
            c.entity = s;
            return Json(BsBranchArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllranches(string id)
        {
            return Json(BsBranchArchivesService.Search(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchEdit(string id)
        {
            BsBranchArchives entity=new BsBranchArchives();
            if(string.IsNullOrEmpty(id)==false)
            {
                entity = BsBranchArchivesService.GetById(id);
            }
            return View(entity);
        }

        public JsonResult SaveBranchArchive(BsBranchArchives s)
        {
            if(s.HaveId)
            {
                BsBranchArchivesService.Update(s);
            }
            else
            {
                s.Id = Guid.NewGuid().ToString();
                BsBranchArchivesService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchDelete(List<string> ids)
        {
            BsBranchArchivesService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 商品档案

        public IGoodsArchivesService GoodsArchivesService { get; set; }
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
                entity.IfExamine = entity.IfExamine == "true" ? "false" : "true";
                GoodsArchivesService.Update(entity);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchGoodsArchiveList(SearchDtoBase<GoodsArchives> c, GoodsArchives s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(GoodsArchivesService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }

            c.entity = s;
            if (Common.MyEnv.IsSupplierLogin)
            {
                c.entity.SupCode = Common.MyEnv.CurrentSupplier.Id;
            }
            return Json(GoodsArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGoodsArchive(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Request["key"];
            }
            var result=GoodsArchivesService.Search(id);
            if (Common.MyEnv.IsSupplierLogin)
            {
                result = result.Where(p => p.SupCode == Common.MyEnv.CurrentSupplier.Id).ToList();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsEdit(string id)
        {
            GoodsArchives entity = new GoodsArchives();
            entity.IfNew = "true";
            if (string.IsNullOrEmpty(id) == false)
            {
                entity = GoodsArchivesService.GetById(id);
            }
            return View(entity);
        }
        public JsonResult SaveGoodsArchive(GoodsArchives s)
        {
            if (s.HaveId)
            {
                GoodsArchivesService.Update(s);
            }
            else
            {
                s.GoodsSubCode = GoodsArchivesService.GenarateGbCode();
                s.Id = GoodsArchivesService.GenarateId(s);
                GoodsArchivesService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GoodsDelete(List<string> ids)
        {
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
