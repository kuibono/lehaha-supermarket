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
    public class TestController : Controller
    {
        //
        // GET: /Test/

        private ISysLoginPowerService SysLoginPowerService { get; set; }
        private ISysEnterpriseArchivesService SysEnterpriseArchivesService { get; set; }
        public ActionResult Index()
        {
            var setting = SysEnterpriseArchivesService.Get();
            if (Common.MyEnv.IsSupplierLogin)
            {
                setting.EnName = Common.MyEnv.CurrentSupplier.SupName;
            }
            return View(setting);
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult LoginPowerList(SearchDtoBase<SysLoginPower> c,SysLoginPower s)
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
            SysLoginPower lp=new SysLoginPower();
            if(id!=null&&id.Length>0)
            {
                lp = SysLoginPowerService.GetById(id);
            }
            return View(lp);
        }

        public ActionResult Save(SysLoginPower sp)
        {
            if(sp.HaveId)
            {
                SysLoginPowerService.Update(sp);
            }
            else
            {
                SysLoginPowerService.Create(sp);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
