using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Spring.Web.Mvc;
using System.Web.Security;
using NHibernate;
using NHibernate.Context;
using Spring.Data.NHibernate;
using NHibernate.Cfg;
namespace TEWorkFlow.Web.Client
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //initializeNHibernateForAppWPF();
            //ISession session = SessionFactory.OpenSession();
            //session.BeginTransaction();
            //CurrentSessionContext.Bind(session);
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            base.Application_BeginRequest(sender, e);
            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SessionId";
                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch { }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;
                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }
            }
            catch { }
        }


        #region  NHibernate

        public static ISessionFactory SessionFactory;

        private static void initializeNHibernateForAppWPF()
        {
            Configuration cfg = new Configuration().Configure();
            //cfg.AddAssembly(typeof(AbstractSession).Assembly);
            cfg.AddProperties(new Dictionary<string, string>
                                  {
                                     {"current_session_context_class","NHibernate.Context.ThreadStaticSessionContext"}
                                  });
            try
            {
                SessionFactory = cfg.BuildSessionFactory();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        /// <summary>
        /// 定义应用程序在退出时要处理的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Application_Exit(object sender, EventArgs e)
        //{
        //    ISession session = CurrentSessionContext.Unbind(SessionFactory);
        //    if (session.Transaction != null)
        //    {
        //        session.Transaction.Commit();
        //        session.Transaction.Dispose();
        //    }
        //}

        private void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (null == cookie)
            {
                cookie = new HttpCookie(cookie_name);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }
    }



}