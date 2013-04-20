using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEWorkFlow.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //RedirectToAction("Index","Test");
            return RedirectToAction("Index", "Test");
        }

    }
}
