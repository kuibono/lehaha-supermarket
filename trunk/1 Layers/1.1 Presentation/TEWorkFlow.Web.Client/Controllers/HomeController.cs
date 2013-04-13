using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TEWorkFlow.Web.Client.Common;
using NSH.Authorization.Service;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class HomeController : Controller
    {

        public IUserService UserService { get; set; }

        public IModuleService ModuleService { get; set; }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Response.Redirect("/");
            return Json(new { success=true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModuleList()
        {
            var user = UserService.GetById(Session[AuthorizeSettings.SessionUserID].ToString().AsInt());
            var module = ModuleService.GetById(7);
            //var moduleList = module.Children;
            var moduleList = CheckModule(module.Children.Where(o => o.IsEnable), user.ModuleList);
            return Json(new { modules = CreateModuleJson(moduleList) }, JsonRequestBehavior.AllowGet);
        }

        private IList<Module> CheckModule(IEnumerable<Module> allModuleList, IList<Module> userModuleList)
        {
            var list = new List<Module>();
            foreach (var module in allModuleList)
            {
                var checkedChildren = CheckModule(module.Children.Where(o => o.IsEnable), userModuleList);
                if (userModuleList.Any(o => o.Id == module.Id) || checkedChildren.Count > 0)
                {
                    list.Add(new Module()
                    {
                        Id = module.Id,
                        ModuleName = module.ModuleName,
                        Path = module.Path,
                        Icon = module.Icon,
                        Children = checkedChildren
                    });
                }
            }
            return list;
        }

        private object[] CreateModuleJson(IList<Module> moduleList)
        {
            var list = new List<object>();
            foreach (var module in moduleList)
            {
                list.Add(new
                {
                    id = module.Id,
                    name = module.ModuleName,
                    path = module.Path,
                    icon = module.Icon,
                    modules = (module.Children != null && module.Children.Count > 0) ? CreateModuleJson(module.Children) : null
                });
            }
            return list.ToArray();
        }
    }
}
