using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TEWorkFlow.Web.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(

             "Localization", // 路由名称  
             "{lang}/{controller}/{action}/{id}", // 带有参数的 URL  
             new { controller = "Home", action = "Index", id = UrlParameter.Optional },//参数默认值  
             new { lang = @"\w{2,3}(-\w{4})?(-\w{2,3})?$" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}