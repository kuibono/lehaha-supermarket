using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Application.Service.Archives;
using Spring.Context;
using Spring.Context.Support;
using TEWorkFlow.Application.Service.Sys;
using Spring.Data.NHibernate.Generic;

namespace TEWorkFlow.Web.Client.Common
{
    public class MyEnv
    {
        private static IApplicationContext _applicationContext = ContextRegistry.GetContext();

        //public IEmemployeearchiveService EmemployeearchiveService = new EmemployeearchiveService();
        //public IFbSupplierArchivesService FbSupplierArchivesService = new FbSupplierArchivesService();


        public static int CurentUserType
        {
            get
            {
                object session = System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserType];
                if (session == null)
                {
                    return int.MinValue;
                }
                return Convert.ToInt32(session);
            }
        }

        public static bool IsHistoryEmloyee
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["userType"];
                if (cookie == null || cookie.Value == null)
                {
                    return false;
                }
                string value = cookie.Value.ToString();
                if (value == "e")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool IsHistorySupplier
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["userType"];
                if (cookie == null || cookie.Value == null)
                {
                    return false;
                }
                string value = cookie.Value.ToString();
                if (value == "s")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsEmployeeLogin
        {
            get
            {
                return CurentUserType == 0;
            }
        }
        public static bool IsSupplierLogin
        {
            get
            {
                return CurentUserType == 1;
            }
        }

        public static FbSupplierArchives CurrentSupplier
        {
            get
            {
                if (CurentUserType == 0 || System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID] == null)
                {
                    return null;
                }
                else
                {
                    return GetFbSupplierArchivesService().GetById(System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID].ToString());
                }
            }
        }

        public static Ememployeearchive CurrentEmployee
        {
            get
            {
                if (CurentUserType == 1 || System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID] == null)
                {
                    return null;
                }
                else
                {
                    return GetEmemployeearchiveService().GetById(System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID].ToString());
                }
            }
        }


        #region GetServices

        public static IEmemployeearchiveService GetEmemployeearchiveService()
        {
            return (IEmemployeearchiveService)_applicationContext.GetObject("EmemployeearchiveService");
        }

        public static IFbSupplierArchivesService GetFbSupplierArchivesService()
        {
            return (IFbSupplierArchivesService)_applicationContext.GetObject("FbSupplierArchivesService");
        }
        public static IFbPaBaseSetService GetFbPaBaseSetService()
        {
            return (IFbPaBaseSetService)_applicationContext.GetObject("FbPaBaseSetService");
        }
        public static HibernateTemplate GetHibernateTemplate()
        {
            return (HibernateTemplate)_applicationContext.GetObject("HibernateTemplate");
        }
        #endregion
    }

    //public class Env
    //{

    //    public static int CurentUserType
    //    {
    //        get
    //        {
    //            MyEnv e = new MyEnv();
    //            return e.CurentUserType;
    //        }
    //    }

    //    public static bool IsEmployeeLogin
    //    {
    //        get
    //        {
    //            MyEnv e = new MyEnv();
    //            return CurentUserType == 0;
    //        }
    //    }
    //    public static bool IsSupplierLogin
    //    {
    //        get
    //        {
    //            MyEnv e = new MyEnv();
    //            return CurentUserType == 1;
    //        }
    //    }

    //    public static FbSupplierArchives CurrentSupplier
    //    {
    //        get
    //        {
    //            MyEnv e = new MyEnv();
    //            return e.CurrentSupplier;
    //        }
    //    }

    //    public Ememployeearchive CurrentEmployee
    //    {
    //        get
    //        {
    //            MyEnv e = new MyEnv();
    //            return e.CurrentEmployee;
    //        }
    //    }
    //}
}