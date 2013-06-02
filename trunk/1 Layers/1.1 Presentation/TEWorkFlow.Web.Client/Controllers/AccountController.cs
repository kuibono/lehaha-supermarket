﻿using System;
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
using TEWorkFlow.Dto;
using TEWorkFlow.Application.Service.Archives;

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
        public ISysEnterpriseArchivesService SysEnterpriseArchivesService { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }
        public IEmemployeearchiveService EmemployeearchiveService { get; set; }
        public IFbSupplierArchivesService FbSupplierArchivesService { get; set; }
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
            m1.SupplierVisible = true;
            m1.EmployeeVisible = true;

            SysmodulecontentService.Create(m1);

            #region 系统设置
            m1 = new Sysmodulecontent()
                     {
                         Icon = "icon-node",
                         Id = "system",
                         ParentId = "root",
                         Url = "",
                         Windowname = "系统设置",
                         EmployeeVisible = true,
                         SupplierVisible = false
                     };
            SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-user",
            //    Id = "users",
            //    ParentId = "system",
            //    Url = "/Test/List",
            //    Windowname = "用户管理",
            //    EmployeeVisible = true,
            //    SupplierVisible = true
            //};
            //SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-bricks",
                Id = "suptypelist",
                ParentId = "system",
                Url = "/Category/SupTypeList",
                Windowname = "行业管理",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-brick",
                Id = "paclasslist",
                ParentId = "system",
                Url = "/Category/PaClassList",
                Windowname = "分店类型",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-group-link",
                Id = "empapoliticslist",
                ParentId = "system",
                Url = "/Category/EmPaPoliticsList",
                Windowname = "员工政治面貌",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-map",
                Id = "BsAreaList",
                ParentId = "system",
                Url = "/Category/BsPaAreaList",
                Windowname = "区域管理",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-drive-user",
                Id = "nationlist",
                ParentId = "system",
                Url = "/Category/NationList",
                Windowname = "员工民族",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-coins",
                Id = "classlist",
                ParentId = "system",
                Url = "/Category/ClassList",
                Windowname = "商品分类",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            #endregion

            #region 档案管理
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-vcard",
                Id = "archives",
                ParentId = "root",
                Url = "",
                Windowname = "档案管理",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-vcard-edit",
                Id = "user_archives",
                ParentId = "archives",
                Url = "/Archives/EmployeeList",
                Windowname = "公司人员档案",
                Index = 11,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-user-suit",
                Id = "supplier_archives",
                ParentId = "archives",
                Url = "/Archives/SupplierList",
                Windowname = "供应商档案",
                Index = 12,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-chart-organisation",
                Id = "branch_archives",
                ParentId = "archives",
                Url = "/Archives/BranchList",
                Windowname = "链锁店档案",
                Index = 13,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart",
            //    Id = "goods_archives",
            //    ParentId = "archives",
            //    Url = "/Archives/GoodsList",
            //    Windowname = "商品档案",
            //    Index = 14,
            //    EmployeeVisible = true,
            //    SupplierVisible = false
            //};
            //SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "goods_archivestree",
                ParentId = "archives",
                Url = "/Archives/GoodsListTree",
                Windowname = "商品档案",
                Index = 15,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);
            #endregion

            #region 业务管理
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-car",
                Id = "business",
                ParentId = "root",
                Url = "",
                Windowname = "业务管理",
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "business-purchase",
                ParentId = "business",
                Url = "/Business/PurchaseList",
                Windowname = "超市订单",
                EmployeeVisible = false,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart-delete",
                Id = "business-return",
                ParentId = "business",
                Url = "/Business/ReturnList",
                Windowname = "产品退货",
                EmployeeVisible = false,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart-add",
                Id = "business-supplement",
                ParentId = "business",
                Url = "/Business/SupplementList",
                Windowname = "产品补货",
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);
            #endregion

            #region 数据查询
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-search",
                Id = "query",
                ParentId = "root",
                Url = "",
                Windowname = "数据查询",
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-purchase",
                ParentId = "query",
                Url = "/Query/PurchaseQuery",
                Windowname = "超市订单",
                Index = 21,
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-branch-order-purchase",
                ParentId = "query",
                Url = "/Query/BranchPurchaseOrder",
                Windowname = "分店订货单据",
                Index = 21,
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-branch-order-goods",
                ParentId = "query",
                Url = "/Query/BranchPurchaseGoods",
                Windowname = "分店订货商品",
                Index = 22,
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-branch-order-supplier",
                ParentId = "query",
                Url = "/Query/BranchPurchaseSupplier",
                Windowname = "分店供货商订货",
                Index = 23,
                EmployeeVisible = true,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-goods",
                ParentId = "query",
                Url = "/Query/GoodsArchiveList",
                Windowname = "产品查询",
                Index = 24,
                EmployeeVisible = false,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);
            
            #endregion

            return null;
        }

        public JsonResult GetModules()
        {
            if (Session[AuthorizeSettings.SessionUserType] == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            if (Session[AuthorizeSettings.SessionUserType].ToString() == "0")
            {
                return Json(
                    SysmodulecontentService.ModelListToDto(
                        SysmodulecontentService.GetAll().Where(p => p.ParentId.Length > 0 && p.EmployeeVisible).OrderBy(p => p.Index).ToList()
                        ),
                    JsonRequestBehavior.AllowGet);
            }
            else if (Session[AuthorizeSettings.SessionUserType].ToString() == "1")
            {
                return Json(
                    SysmodulecontentService.ModelListToDto(
                        SysmodulecontentService.GetAll().Where(p => p.ParentId.Length > 0 && p.SupplierVisible).OrderBy(p => p.Index).ToList()
                        ),
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SLogin()
        {
            return View();
        }
        public ActionResult ELogin()
        {
            return View();
        }

        public ActionResult VCode()
        {
            VerifyCode v = new VerifyCode();
            string vcode = VerifyCode.GetRandomNumber(1000, 9999).ToString();
            Session["SafeCode"] = vcode;
            v.CreateImageOnPage(vcode, System.Web.HttpContext.Current);
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


            LoginResult result = SysLoginPowerService.CheckUser(model.UserName, model.Password, model.logintype);
            if (result.IsSuccess)
            {
                if (result.Employee != null)
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Employee.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Employee.Id;
                    Session[AuthorizeSettings.SessionUserType] = "0";

                }
                else
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Supplier.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Supplier.Id;
                    Session[AuthorizeSettings.SessionUserType] = "1";
                }
                return RedirectToAction("Index", "Test");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }


            //var u = SysLoginPowerService.CheckUser(model.UserName, model.Password);
            //if (u == null)
            //{
            //    ModelState.AddModelError("", "账号或密码错误，请重试");
            //    return View(model);
            //}
            //else
            //{
            //    Session[AuthorizeSettings.SessionUserName] = u.Username;
            //    Session[AuthorizeSettings.SessionUserID] = u.Id;
            //    return RedirectToAction("Index", "Test");
            //}

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

        [HttpPost]
        public ActionResult SLogin(LoginModel model, string returnUrl)
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


            LoginResult result = SysLoginPowerService.CheckUser(model.UserName, model.Password, model.logintype);
            if (result.IsSuccess)
            {
                if (result.Employee != null)
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Employee.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Employee.Id;
                    Session[AuthorizeSettings.SessionUserType] = "0";

                }
                else
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Supplier.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Supplier.Id;
                    Session[AuthorizeSettings.SessionUserType] = "1";
                }
                return RedirectToAction("Index", "Test");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult ELogin(LoginModel model, string returnUrl)
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


            LoginResult result = SysLoginPowerService.CheckUser(model.UserName, model.Password, model.logintype);
            if (result.IsSuccess)
            {
                if (result.Employee != null)
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Employee.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Employee.Id;
                    Session[AuthorizeSettings.SessionUserType] = "0";

                }
                else
                {
                    Session[AuthorizeSettings.SessionUserName] = result.Supplier.LoginName;
                    Session[AuthorizeSettings.SessionUserID] = result.Supplier.Id;
                    Session[AuthorizeSettings.SessionUserType] = "1";
                }
                return RedirectToAction("Index", "Test");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }

        }

        public ActionResult DepartmentSelectTree()
        {
            return View();
        }

        public ActionResult SysSetting()
        {
            return View(SysEnterpriseArchivesService.Get());
        }

        public JsonResult SaveSysSetting(SysEnterpriseArchives setting)
        {
            SysEnterpriseArchivesService.Save(setting);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            if (Common.MyEnv.IsEmployeeLogin)
            {
                Session.Abandon();
                return RedirectToAction("ELogin", "Account");
            }
            if (Common.MyEnv.IsSupplierLogin)
            {
                Session.Abandon();
                return RedirectToAction("SLogin", "Account");
            }
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult BaseSetting()
        {
            return View(FbPaBaseSetService.Get());
        }

        public JsonResult SaveBaseSetting(FbPaBaseSet setting)
        {
            setting.Id = "1";
            setting.Operator = "1";
            setting.OperatorDate = DateTime.Now;
            FbPaBaseSetService.Save(setting);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string old,string newpass,string confirmpass)
        {
            if (Common.MyEnv.IsEmployeeLogin == false && Common.MyEnv.IsSupplierLogin == false)
            {
                return Json(new Result() { Success = false, Text = "没有登录，请登录后修改密码！" }, JsonRequestBehavior.AllowGet);
            }

            if (newpass != confirmpass)
            {
                return Json(new Result() { Success = false, Text = "新密码和确认密码输入不一致，请重新输入" }, JsonRequestBehavior.AllowGet);
            }

            if (Common.MyEnv.IsEmployeeLogin)
            {
                //员工
                var currentEmployee = Common.MyEnv.CurrentEmployee;
                if (old != currentEmployee.LoginPass)
                {
                    return Json(new Result() { Success = false, Text = "旧密码输入错误，请重新输入" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    currentEmployee.LoginPass = newpass;
                    EmemployeearchiveService.Update(currentEmployee);
                    return Json(new Result() { Success = true, Text = "密码修改成功！" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //供货商
                var currentSupplier = Common.MyEnv.CurrentSupplier;
                if (old != currentSupplier.LoginPass)
                {
                    return Json(new Result() { Success = false, Text = "旧密码输入错误，请重新输入" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    currentSupplier.LoginPass = newpass;
                    FbSupplierArchivesService.Update(currentSupplier);
                    return Json(new Result() { Success = true, Text = "密码修改成功！" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}