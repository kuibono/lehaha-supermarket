using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Application.Service.Archives;

namespace TEWorkFlow.Web.Client.Common
{
    public class MyEnv
    {

        public IEmemployeearchiveService EmemployeearchiveService = new EmemployeearchiveService();
        public IFbSupplierArchivesService FbSupplierArchivesService = new FbSupplierArchivesService();


        public int CurentUserType
        {
            get
            {
                return Convert.ToInt32(System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserType]);
            }
        }

        public bool IsEmployeeLogin
        {
            get
            {
                return CurentUserType == 0;
            }
        }
        public bool IsSupplierLogin
        {
            get
            {
                return CurentUserType == 1;
            }
        }

        public FbSupplierArchives CurrentSupplier
        {
            get
            {
                if (CurentUserType == 0 || System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID] == null)
                {
                    return null;
                }
                else
                {
                    return FbSupplierArchivesService.GetById(System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID].ToString());
                }
            }
        }

        public Ememployeearchive CurrentEmployee
        {
            get
            {
                if (CurentUserType == 1 || System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID] == null)
                {
                    return null;
                }
                else
                {
                    return EmemployeearchiveService.GetById(System.Web.HttpContext.Current.Session[AuthorizeSettings.SessionUserID].ToString());
                }
            }
        }
    }

    public class Env
    {
        
        public static int CurentUserType
        {
            get
            {
                MyEnv e = new MyEnv();
                return e.CurentUserType;
            }
        }

        public static bool IsEmployeeLogin
        {
            get
            {
                MyEnv e = new MyEnv();
                return CurentUserType == 0;
            }
        }
        public static bool IsSupplierLogin
        {
            get
            {
                MyEnv e = new MyEnv();
                return CurentUserType == 1;
            }
        }

        public static FbSupplierArchives CurrentSupplier
        {
            get
            {
                MyEnv e = new MyEnv();
                return e.CurrentSupplier;
            }
        }

        public Ememployeearchive CurrentEmployee
        {
            get
            {
                MyEnv e = new MyEnv();
                return e.CurrentEmployee;
            }
        }
    }
}