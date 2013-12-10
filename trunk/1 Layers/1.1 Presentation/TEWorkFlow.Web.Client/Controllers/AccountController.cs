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
using TEWorkFlow.Dto;
using TEWorkFlow.Application.Service.Archives;
using System.Data;
using System.Configuration;
using System.Threading;

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

            #region 员工菜单

            #region 数据查询
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-search",
                Id = "em-query",
                ParentId = "root",
                Url = "",
                Windowname = "数据查询",
                EmployeeVisible = true,
                Index = 1
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-supplier-purchase",
                ParentId = "em-query",
                Url = "/Report/SupplierPurchase",
                Windowname = "供货商订单查询统计",
                Index = 11,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-supplier-purchase-history",
                ParentId = "em-query",
                Url = "/Report/SupplierPurchaseHistory",
                Windowname = "供货商历史订单查询",
                Index = 12,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-purchase",
                ParentId = "em-query",
                Url = "/Report/BranchPurchase",
                Windowname = "分店订单查询统计",
                Index = 13,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-purchase-history",
                ParentId = "em-query",
                Url = "/Report/BranchPurchaseHistory",
                Windowname = "分店历史订单查询",
                Index = 14,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-goods",
                ParentId = "em-query",
                Url = "/Query/BranchPurchaseGoods",
                Windowname = "分店商品统计",
                Index = 15,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-goods-retail",
                ParentId = "em-query",
                Url = "/Report/BranchRetail/",
                Windowname = "分店销售数据统计",
                Index = 16,
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-purchase-retail-compare",
                ParentId = "em-query",
                Url = "/Report/BranchPurchaseRetailCompare/",
                Windowname = "分店采购销售对比",
                Index = 17,
                EmployeeVisible = true
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
                Index = 2
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
                Windowname = "连锁店档案",
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

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-chart-organisation",
                Id = "SupplierBranchRelationManagement",
                ParentId = "archives",
                Url = "/Archives/SupplierBranchRelationManagement",
                Windowname = "供货商分店关系管理",
                Index = 16,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-chart-organisation",
                Id = "BranchSupplierRelationManagement",
                ParentId = "archives",
                Url = "/Archives/BranchSupplierRelationManagement",
                Windowname = "分店供货商关系管理",
                Index = 16,
                EmployeeVisible = true,
                SupplierVisible = false
            };
            SysmodulecontentService.Create(m1);
            #endregion

            #region 补货管理
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-car",
                Id = "business-em",
                ParentId = "root",
                Url = "",
                Windowname = "补货管理",
                EmployeeVisible = true,
                Index = 3
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart-add",
                Id = "business-supplement",
                ParentId = "business-em",
                Url = "/Business/SupplementList",
                Windowname = "产品补货",
                EmployeeVisible = true
            };
            SysmodulecontentService.Create(m1);
            #endregion

            #region 系统设置
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-node",
                Id = "system",
                ParentId = "root",
                Url = "",
                Windowname = "系统设置",
                EmployeeVisible = true,
                Index = 4
            };
            SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-user",
            //    Id = "users",
            //    ParentId = "system",
            //    Url = "/Home/List",
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
                SupplierVisible = false,
                Index = 41
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
                SupplierVisible = false,
                Index = 42
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
                SupplierVisible = false,
                Index = 43
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
                SupplierVisible = false,
                Index = 44
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
                SupplierVisible = false,
                Index = 45
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
                SupplierVisible = false,
                Index = 46
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cog-edit",
                Id = "bussiness-setting",
                ParentId = "system",
                Url = "/Account/BaseSetting/",
                Windowname = "业务设置",
                EmployeeVisible = true,
                SupplierVisible = false,
                OpenInNewWindow = true,
                Index = 47
            };
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-edit",
                Id = "websystem-setting",
                ParentId = "system",
                Url = "/Account/SysSetting/",
                Windowname = "系统设置",
                EmployeeVisible = true,
                SupplierVisible = false,
                OpenInNewWindow = true,
                Index = 48
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-bell",
                Id = "system-postlist",
                ParentId = "system",
                Url = "/Home/PostList/",
                Windowname = "公告管理",
                EmployeeVisible = true,
                SupplierVisible = false,
                OpenInNewWindow = false,
                Index = 48
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-bell",
                Id = "system-branchpostlist",
                ParentId = "system",
                Url = "/Home/BranchPostList/",
                Windowname = "分店通知",
                EmployeeVisible = true,
                SupplierVisible = false,
                OpenInNewWindow = false,
                Index = 49
            };
            SysmodulecontentService.Create(m1);
            #endregion

            #endregion






            #region 业务管理
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-car",
                Id = "business",
                ParentId = "root",
                Url = "",
                Windowname = "业务管理",
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
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart-delete",
            //    Id = "business-return",
            //    ParentId = "business",
            //    Url = "/Business/ReturnList",
            //    Windowname = "产品退货",
            //    SupplierVisible = true
            //};
            //SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart-add",
            //    Id = "business-supplement",
            //    ParentId = "business",
            //    Url = "/Business/SupplementList",
            //    Windowname = "产品补货",
            //    EmployeeVisible = true,
            //    SupplierVisible = false
            //};
            //SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-goods",
                ParentId = "business",
                Url = "/Query/GoodsArchiveList",
                Windowname = "产品管理",
                Index = 24,
                EmployeeVisible = false,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "query-supplier_branch",
                ParentId = "business",
                Url = "/Archives/BranchList",
                Windowname = "分店查看",
                Index = 25,
                EmployeeVisible = false,
                SupplierVisible = true
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
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);
            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-purchase-sup",
                ParentId = "query",
                Url = "/Report/BranchPurchase",
                Windowname = "分店订单查询统计",
                Index = 31,
                SupplierVisible = true,
                EmployeeVisible = false
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-purchase-sup-history",
                ParentId = "query",
                Url = "/Report/BranchPurchaseHistory",
                Windowname = "分店历史订单查询",
                Index = 32,
                SupplierVisible = true
            };
            SysmodulecontentService.Create(m1);

            m1 = new Sysmodulecontent()
            {
                Icon = "icon-cart",
                Id = "report-branch-goods-sup",
                ParentId = "query",
                Url = "/Query/BranchPurchaseGoods",
                Windowname = "分店商品统计",
                Index = 33,
                SupplierVisible = true,
                EmployeeVisible = false
            };
            SysmodulecontentService.Create(m1);
            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart",
            //    Id = "query-purchase",
            //    ParentId = "query",
            //    Url = "/Query/PurchaseQuery",
            //    Windowname = "超市订单",
            //    Index = 21,
            //    SupplierVisible = true
            //};
            //SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart",
            //    Id = "query-branch-order-purchase",
            //    ParentId = "query",
            //    Url = "/Query/BranchPurchaseOrder",
            //    Windowname = "分店订货单据",
            //    Index = 21,
            //    SupplierVisible = true
            //};
            //SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart",
            //    Id = "query-branch-order-goods",
            //    ParentId = "query",
            //    Url = "/Query/BranchPurchaseGoods",
            //    Windowname = "分店订货商品",
            //    Index = 33,
            //    SupplierVisible = true,
            //    EmployeeVisible = false
            //};
            //SysmodulecontentService.Create(m1);

            //m1 = new Sysmodulecontent()
            //{
            //    Icon = "icon-cart",
            //    Id = "query-branch-order-supplier",
            //    ParentId = "query",
            //    Url = "/Query/BranchPurchaseSupplier",
            //    Windowname = "分店供货商订货",
            //    Index = 23,
            //    EmployeeVisible = true,
            //    SupplierVisible = true
            //};
            //SysmodulecontentService.Create(m1);



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

        public JsonResult ReIniAllSupplierLoginNameAndPassword()
        {
            string connstr = ConfigurationManager.AppSettings["connectionString"];
            SqlHelper Helper = new SqlHelper(connstr);
            DataTable allSupplier = Helper.ExecuteDataTable(CommandType.Text, "select sup_code from fb_supplier_archives");

            List<int> existName = new List<int>();

            for (int i = 0; i < allSupplier.Rows.Count; i++)
            {
                int name = NSH.VSTO.Extend.GetRandomNumber(10000,99999);

                while (existName.Any(p => p == name))
                {
                    name = NSH.VSTO.Extend.GetRandomNumber(10000, 99999);
                }
                Helper = new SqlHelper(connstr);
                string str_sql = string.Format("update fb_supplier_archives set  login_name='{0}', login_pass='{1}' where sup_code={2}", name, "888888", allSupplier.Rows[i]["sup_code"].ToString());
                Helper.ExecuteNonQuery(CommandType.Text, str_sql);
                Response.Buffer = false;
                Response.Write(name + "<br/>");

                Thread.Sleep(13);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
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
                SetUserType(false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }
            //if (String.IsNullOrWhiteSpace(model.UserName))
            //{
            //    ModelState.AddModelError("", "账号不能为空");
            //    return View(model);
            //}

            //if (String.IsNullOrWhiteSpace(model.Password))
            //{
            //    ModelState.AddModelError("", "密码不能为空");
            //    return View(model);
            //}
            //if (String.IsNullOrWhiteSpace(model.VCode))
            //{
            //    ModelState.AddModelError("", "验证码不能为空");
            //    return View(model);
            //}
            //if (Session["SafeCode"] == null || Session["SafeCode"].ToString() != model.VCode)
            //{
            //    ModelState.AddModelError("", "验证码错误");
            //    Session.Remove("SafeCode");
            //    return View(model);
            //}
            //Session.Remove("SafeCode");//删除验证码Session 防止机器登录


            //LoginResult result = SysLoginPowerService.CheckUser(model.UserName, model.Password, model.logintype);
            //if (result.IsSuccess)
            //{
            //    if (result.Employee != null)
            //    {
            //        Session[AuthorizeSettings.SessionUserName] = result.Employee.LoginName;
            //        Session[AuthorizeSettings.SessionUserID] = result.Employee.Id;
            //        Session[AuthorizeSettings.SessionUserType] = "0";

            //    }
            //    else
            //    {
            //        Session[AuthorizeSettings.SessionUserName] = result.Supplier.LoginName;
            //        Session[AuthorizeSettings.SessionUserID] = result.Supplier.Id;
            //        Session[AuthorizeSettings.SessionUserType] = "1";
            //    }
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "账号或密码错误，请重试");
            //    return View(model);
            //}


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
            //    return RedirectToAction("Index", "Home");
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
                SetUserType(false);
                return RedirectToAction("Index", "Home");
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
                SetUserType(true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", result.Text);
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
            if (Common.MyEnv.IsHistoryEmloyee)
            {
                Session.Abandon();
                return RedirectToAction("ELogin", "Account");
            }
            if (Common.MyEnv.IsHistorySupplier)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public void SetUserType(bool isEmployee)
        {
            HttpCookie cookie = new HttpCookie("userType");
            cookie.Value = isEmployee ? "e" : "s";
            cookie.Expires = DateTime.Now.AddMonths(1);
            cookie.Path = "/";
            HttpContext.Response.SetCookie(cookie);
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
        public JsonResult ChangePassword(string old, string newpass, string confirmpass)
        {
            if (Common.MyEnv.IsEmployeeLogin == false && Common.MyEnv.IsSupplierLogin == false)
            {
                return Json(new cResult() { Success = false, Text = "没有登录，请登录后修改密码！" }, JsonRequestBehavior.AllowGet);
            }

            if (newpass != confirmpass)
            {
                return Json(new cResult() { Success = false, Text = "新密码和确认密码输入不一致，请重新输入" }, JsonRequestBehavior.AllowGet);
            }

            if (Common.MyEnv.IsEmployeeLogin)
            {
                //员工
                var currentEmployee = Common.MyEnv.CurrentEmployee;
                if (old != currentEmployee.LoginPass)
                {
                    return Json(new cResult() { Success = false, Text = "旧密码输入错误，请重新输入" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    currentEmployee.LoginPass = newpass;
                    EmemployeearchiveService.Update(currentEmployee);
                    return Json(new cResult() { Success = true, Text = "密码修改成功！" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //供货商
                var currentSupplier = Common.MyEnv.CurrentSupplier;
                if (old != currentSupplier.LoginPass)
                {
                    return Json(new cResult() { Success = false, Text = "旧密码输入错误，请重新输入" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    currentSupplier.LoginPass = newpass;
                    FbSupplierArchivesService.Update(currentSupplier);
                    return Json(new cResult() { Success = true, Text = "密码修改成功！" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
