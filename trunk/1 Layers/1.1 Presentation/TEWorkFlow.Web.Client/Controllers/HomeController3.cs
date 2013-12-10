using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorizeAttribute]
    public class HomeController3 : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //RedirectToAction("Index","Test");
            return RedirectToAction("Index", "Home");
        }

    }
}
