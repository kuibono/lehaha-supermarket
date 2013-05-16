using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.Category;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Web.Client.Models;
//using NSH.Authorization.Service;
using System.Web.Security;
using TEWorkFlow.Web.Client.Common;
//using TEWorkFlow.Application.Service.WorkFlow;
using TEWorkFlow.Application.Service.Sys;
///using NSH.Core.WPF;
using NSH.VSTO;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class AccountController : Controller
    {
        //public IUserService UserService { get; set; }

        public ISysLoginPowerService SysLoginPowerService { get; set; }
        public ISysmodulecontentService SysmodulecontentService { get; set; }
        public IBsPaClassService BsPaClassService { get; set; }
        public IBsPaAreaService BsPaAreaService { get; set; }
        public ISysPaDepartmentService SysPaDepartmentService { get; set; }
        public ActionResult IniAll()
        {
            IniUser();
            IniUserS();
            InISysPaClassAndArea();
            InISysModule();
            return Json("成功", JsonRequestBehavior.AllowGet);
        }

        public ActionResult IniUser()
        {
            SysLoginPower u = new SysLoginPower();
            u.Emname = "管理员";
            u.Id = "default";
            u.Ifcash = "false";
            u.Maxdiscount = 0;
            u.UserType = 0;
            u.Username = "sysadmin";
            u.Userpw = "1";
            u.Userstate = "0";
            SysLoginPowerService.Create(u);
            return null;
        }

        public ActionResult IniUserS()
        {
            for (int i = 0; i < 50; i++)
            {
                SysLoginPower u = new SysLoginPower();
                u.Emname = "管理员" + i;
                u.Id = i.ToString();
                u.Ifcash = "false";
                u.Maxdiscount = 0;
                u.UserType = i % 2;
                u.Username = "user" + i;
                u.Userpw = "1";
                u.Userstate = "0";
                SysLoginPowerService.Create(u);
            }

            return null;
        }

        public ActionResult InISysPaClassAndArea()
        {
            BsPaAreaService.Create(new BsPaArea() { Id = "1", AreaName = "中山" });
            BsPaAreaService.Create(new BsPaArea() { Id = "2", AreaName = "西岗" });
            BsPaAreaService.Create(new BsPaArea() { Id = "3", AreaName = "沙河口" });
            BsPaAreaService.Create(new BsPaArea() { Id = "4", AreaName = "甘井子" });
            BsPaAreaService.Create(new BsPaArea() { Id = "5", AreaName = "开发区" });
            BsPaAreaService.Create(new BsPaArea() { Id = "6", AreaName = "金州" });
            BsPaAreaService.Create(new BsPaArea() { Id = "7", AreaName = "旅顺" });

            BsPaClassService.Create(new BsPaClass() { Id = "1", ClassName = "直营店" });
            BsPaClassService.Create(new BsPaClass() { Id = "2", ClassName = "加盟店" });
            return null;
        }

        public ActionResult InISysModule()
        {
            Sysmodulecontent m1 = new Sysmodulecontent();
            m1.Icon = "";
            m1.Id = "root";
            m1.ParentId = "";
            m1.Url = "";
            m1.Windowname = "root";
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
                     {
                         Icon = "icon-node",
                         Id = "system",
                         ParentId = "root",
                         Url = "",
                         Windowname = "系统设置"
                     };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-user",
                Id = "users",
                ParentId = "system",
                Url = "/Test/List",
                Windowname = "用户管理"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-bricks",
                Id = "suptypelist",
                ParentId = "system",
                Url = "/Category/SupTypeList",
                Windowname = "行业管理"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-brick",
                Id = "paclasslist",
                ParentId = "system",
                Url = "/Category/PaClassList",
                Windowname = "分店类型"
            };
            SysmodulecontentService.Create(m1);


            m1 = new Sysmodulecontent()
            {
                Icon = "icon-vcard",
                Id = "archives",
                ParentId = "root",
                Url = "",
                Windowname = "档案管理"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-vcard-edit",
                Id = "user_archives",
                ParentId = "archives",
                Url = "/Archives/EmployeeList",
                Windowname = "人员档案"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-user-suit",
                Id = "supplier_archives",
                ParentId = "archives",
                Url = "/Archives/SupplierList",
                Windowname = "供应商档案"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-chart-organisation",
                Id = "branch_archives",
                ParentId = "archives",
                Url = "/Archives/BranchList",
                Windowname = "分店档案"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "goods_archives",
                ParentId = "archives",
                Url = "/Archives/GoodsList",
                Windowname = "商品档案"
            };
            SysmodulecontentService.Create(m1);



            m1 = new Sysmodulecontent()
            {
                Icon = "icon-car",
                Id = "business",
                ParentId = "root",
                Url = "",
                Windowname = "业务管理"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "business-purchase",
                ParentId = "business",
                Url = "/Business/PurchaseList",
                Windowname = "产品采购"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart-delete",
                Id = "business-return",
                ParentId = "business",
                Url = "/Business/ReturnList",
                Windowname = "产品退货"
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart-add",
                Id = "business-supplement",
                ParentId = "business",
                Url = "/Business/SupplementList",
                Windowname = "产品补货"
            };
            SysmodulecontentService.Create(m1);
            return null;
        }

        public JsonResult GetModules()
        {
            return Json(
                SysmodulecontentService.ModelListToDto(
                    SysmodulecontentService.GetAll().Where(p => p.ParentId.Length > 0).ToList()
                    ),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult VCode()
        {
            VerifyCode v = new VerifyCode();
            v.CreateImageOnPage(v.CreateForWordCode(), System.Web.HttpContext.Current);
            return null;
        }

        public JsonResult GetDepatmentTree()
        {
            return Json(
            SysPaDepartmentService.ModelListToDto(SysPaDepartmentService.GetAll()),
            JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (String.IsNullOrWhiteSpace(model.UserName))
            {
                ModelState.AddModelError("", "账号不能为空");
                return View(model);
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "密码不能为空");
                return View(model);
            }
            if (String.IsNullOrWhiteSpace(model.VCode))
            {
                ModelState.AddModelError("", "验证码不能为空");
                return View(model);
            }
            if (Session["SafeCode"] == null || Session["SafeCode"].ToString() != model.VCode)
            {
                ModelState.AddModelError("", "验证码错误");
                Session.Remove("SafeCode");
                return View(model);
            }
            Session.Remove("SafeCode");//删除验证码Session 防止机器登录

            var u = SysLoginPowerService.CheckUser(model.UserName, model.Password);
            if (u == null)
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }
            else
            {
                Session[AuthorizeSettings.SessionUserName] = u.Username;
                Session[AuthorizeSettings.SessionUserID] = u.Id;
                return RedirectToAction("Index", "Test");
            }

            //NSH.Authorization.Domain.User user = new NSH.Authorization.Domain.User();
            //user.LoginName = model.UserName;
            //user.Password = model.Password.ToMD5();
            //Session.Remove("SafeCode");//删除验证码Session 防止机器登录
            //if (UserService.CheckLoginUser(ref user))
            //{
            //    Session[AuthorizeSettings.SessionUserName] = user.UserName;
            //    Session[AuthorizeSettings.SessionUserID] = user.Id;
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "账号或密码错误，请重试");
            //    return View(model);
            //}

        }

        public ActionResult DepartmentSelectTree()
        {
            return View();
        }
    }
}
