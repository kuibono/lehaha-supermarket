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

        public ActionResult SupplierDelete(List<string> ids)
        {
            FbSupplierArchivesService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchSupplierArchiveList(SearchDtoBase<FbSupplierArchives> c, FbSupplierArchives s)
        {
            c.entity = s;
            return Json(FbSupplierArchivesService.Search(c), JsonRequestBehavior.AllowGet);
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

        public JsonResult SearchGoodsArchiveList(SearchDtoBase<GoodsArchives> c, GoodsArchives s)
        {
            c.entity = s;
            return Json(GoodsArchivesService.Search(c), JsonRequestBehavior.AllowGet);
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
        #endregion

    }
}
