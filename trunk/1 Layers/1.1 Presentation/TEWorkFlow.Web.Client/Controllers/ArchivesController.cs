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
            string loginName=FbSupplierArchivesService.GenerateLoginName();

            FbSupplierArchives model=new FbSupplierArchives()
                                         {
                                             //Id=Guid.NewGuid() .ToString(),
                                             CreateDate = DateTime.Now,
                                             ExamineDate = DateTime.Now,
                                             OperatorDate = DateTime.Now,
                                             LoginPass = loginName,
                                             LoginName = loginName
                                         };

            if(string.IsNullOrEmpty(id)==false)
            {
                model = FbSupplierArchivesService.GetById(id);
            }
            return View(model);
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

        public JsonResult SaveSupplierArchive(FbSupplierArchives s)
        {
            if(s.HaveId)
            {
                FbSupplierArchivesService.Update(s);
            }
            else
            {
                s.Id = Guid.NewGuid().ToString();
                FbSupplierArchivesService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSupplierArchive(string key)
        {
            return Json(FbSupplierArchivesService.Search(key), JsonRequestBehavior.AllowGet);
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

        public JsonResult SearchAllranches(string key)
        {
            return Json(BsBranchArchivesService.Search(key), JsonRequestBehavior.AllowGet);
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
        public JsonResult SearchGoodsArchiveList(SearchDtoBase<GoodsArchives> c, GoodsArchives s)
        {
            if (string.IsNullOrEmpty(Request["key"]) == false)
            {
                return Json(GoodsArchivesService.Search(Request["key"]), JsonRequestBehavior.AllowGet);
            }

            c.entity = s;
            return Json(GoodsArchivesService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGoodsArchive(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Request["key"];
            }
            return Json(GoodsArchivesService.Search(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsEdit(string id)
        {
            GoodsArchives entity = new GoodsArchives();
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
                s.Id = Guid.NewGuid().ToString();
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

    }
}
