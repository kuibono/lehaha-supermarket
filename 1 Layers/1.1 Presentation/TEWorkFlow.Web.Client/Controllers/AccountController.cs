using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Web.Client.Models;
using NSH.Authorization.Service;
using System.Web.Security;
using TEWorkFlow.Web.Client.Common;
using TEWorkFlow.Application.Service.WorkFlow;
using NSH.Core.WPF;
using NSH.VSTO;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class AccountController : Controller
    {
        public IUserService UserService { get; set; }



        public ActionResult Login()
        {
            return View();
        }
        //public ActionResult Login2()
        //{
        //    return View();
        //}

        public ActionResult VCode()
        {
            VerifyCode v=new VerifyCode();
            v.CreateImageOnPage(v.CreateForWordCode(),System.Web.HttpContext.Current);
            return null;
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

            NSH.Authorization.Domain.User user = new NSH.Authorization.Domain.User();
            user.LoginName = model.UserName;
            user.Password = model.Password.ToMD5();
            Session.Remove("SafeCode");//删除验证码Session 防止机器登录
            if (UserService.CheckLoginUser(ref user))
            {
                Session[AuthorizeSettings.SessionUserName] = user.UserName;
                Session[AuthorizeSettings.SessionUserID] = user.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误，请重试");
                return View(model);
            }

        }
    }
}
