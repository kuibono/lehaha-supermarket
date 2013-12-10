using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Sys;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Dto;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private ISysLoginPowerService SysLoginPowerService { get; set; }
        private ISysEnterpriseArchivesService SysEnterpriseArchivesService { get; set; }
        private ISysPostService SysPostService { get; set; }
        private IBranchPostService BranchPostService { get; set; }
        public ActionResult Index()
        {
            var setting = SysEnterpriseArchivesService.Get();
            if (Common.MyEnv.IsSupplierLogin)
            {
                try
                {

                    setting.TempString = Common.MyEnv.CurrentSupplier.SupName;
                }
                catch
                {
                    setting.TempString = setting.EnName;
                }
            }
            else
            {
                setting.TempString = setting.EnName;
            }
            return View(setting);
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult LoginPowerList(SearchDtoBase<SysLoginPower> c, SysLoginPower s)
        {
            //var q =SysLoginPowerService.GetList()
            c.entity = s;


            return Json(SysLoginPowerService.Search(c), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(List<string> ids)
        {
            SysLoginPowerService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            SysLoginPower lp = new SysLoginPower();
            if (id != null && id.Length > 0)
            {
                lp = SysLoginPowerService.GetById(id);
            }
            return View(lp);
        }

        public ActionResult Save(SysLoginPower sp)
        {
            if (sp.HaveId)
            {
                SysLoginPowerService.Update(sp);
            }
            else
            {
                SysLoginPowerService.Create(sp);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Blank()
        {
            return View();
        }

        public JsonResult GetAllPosts(string id)
        {
            IList<SysPost> result = new List<SysPost>();
            //id 是关键词
            if (Common.MyEnv.IsSupplierLogin)
            {
                result = SysPostService.GetAll(Common.MyEnv.CurrentSupplier.Id).OrderByDescending(p => p.PostTime).ToList();
            }
            else
            {
                result = SysPostService.GetAll(null).OrderByDescending(p => p.PostTime).ToList();
            }
            if (string.IsNullOrEmpty(id) == false)
            {
                result = result.Where(p => p.Title.Contains(id)).ToList();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllBranchPosts(string id)
        {
            IList<BranchPost> result = new List<BranchPost>();
            //id 是关键词
            if (Common.MyEnv.IsSupplierLogin)
            {
                result = BranchPostService.GetAll(Common.MyEnv.CurrentSupplier.Id).OrderByDescending(p => p.PostTime).ToList();
            }
            else
            {
                result = BranchPostService.GetAll(null).OrderByDescending(p => p.PostTime).ToList();
            }
            if (string.IsNullOrEmpty(id) == false)
            {
                result = result.Where(p => p.Title.Contains(id)).ToList();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostList()
        {
            return View();
        }
        public ActionResult BranchPostList()
        {
            return View();
        }
        public ActionResult PostDelete(List<string> ids)
        {
            SysPostService.Delete(ids);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostEdit(string id)
        {
            SysPost p = new SysPost();
            p.PostUser = Common.MyEnv.CurrentEmployee.Emname;
            p.PostTime = DateTime.Now;
            if (string.IsNullOrEmpty(id) == false)
            {
                p = SysPostService.GetById(id);
            }
            return View(p);
        }

        public ActionResult BranchPostEdit(string id)
        {
            BranchPost p = new BranchPost();
            p.PostUser = Common.MyEnv.CurrentEmployee.Emname;
            p.PostTime = DateTime.Now;
            if (string.IsNullOrEmpty(id) == false)
            {
                p = BranchPostService.GetById(id);
            }
            return View(p);
        }

        public JsonResult SavePost(SysPost s)
        {
            if (s.HaveId)
            {
                SysPostService.Update(s);
            }
            else
            {
                s.Id = Guid.NewGuid().ToString();
                SysPostService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveBranchPost(BranchPost s)
        {
            if (s.HaveId)
            {
                BranchPostService.Update(s);
            }
            else
            {
                s.Id = Guid.NewGuid().ToString();
                BranchPostService.Create(s);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PostReaded(string id)
        {
            if (Common.MyEnv.IsSupplierLogin)
            {
                SysPostService.SetPostReaded(id, Common.MyEnv.CurrentSupplier.Id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
