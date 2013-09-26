using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEWorkFlow.Web.Client.Common
{
    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || session[AuthorizeSettings.SessionUserType] == null)
            {
                string url = "/Account/Login";
                if (Common.MyEnv.IsHistoryEmloyee)
                {
                    url = "/Account/ELogin";
                }
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Write("<script type='text/javascript'>top.location.href='" + url + "';</script>");
                filterContext.HttpContext.Response.End();
                //filterContext.Result = new RedirectResult("~/Account/Login");
            }
            //if (session != null)
            //{
            //    if (filterContext.HttpContext.Session[AuthorizeSettings.SessionUserName] == null)
            //    {
            //        if (filterContext.HttpContext.Request.Url != null)
            //        {
            //            filterContext.HttpContext.Session["url"] = filterContext.HttpContext.Request.Url.ToString();
            //        }
            //        filterContext.Result = new RedirectResult("~/Account/Login");
            //    }
            //}


            //var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            //var action = filterContext.RouteData.Values["action"].ToString().ToLower();

            //session[AuthorizeSettings.SessionUserName] = "sysadmin";
            //session[AuthorizeSettings.SessionUserID] = 1;
        }
    }
}