using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TEWorkFlow.Application.Service.WorkFlow;
using TEWorkFlow.Domain.WorkFlow;
using NSH.Authorization.Service;
using NSH.Authorization.Domain;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class WorkStepController : Controller
    {
        //
        // GET: /WorkStep/

        public IWorkStepService WorkStepService { get; set; }

        public IWorkFlowService WorkFlowService { get; set; }

        public IUserService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Query()
        {

            var list = WorkStepService.GetAll().Select(p => new
            {
                p.Id,
                p.Name,
                workflowid = p.WorkFlow.Id,
                workflowname = p.WorkFlow.Name,
                userNames = String.Join(",", p.Users.Select(o => o.UserName).ToArray()),
                uids = p.Users.Select(o => o.Id.ToString()).ToArray()
            });
            return Json(list, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWorkFlow()
        {
            var list = WorkFlowService.GetAll().Select(p => new { p.Id, p.Name });
            return Json(list, "text/html", JsonRequestBehavior.AllowGet);
        }

        public int RequestInt(string key)
        {
            if (Request.Form[key] != null)
            {
                return Convert.ToInt32(Request.Form[key]);
            }
            if (Request.QueryString[key] != null)
            {
                return Convert.ToInt32(Request.QueryString[key]);
            }

            try
            {
                return Convert.ToInt32(Request.Form[key]);
            }
            catch
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString[key]);
                }
                catch
                {
                    return int.MinValue;
                }
            }
        }

        public ActionResult SaveStep()
        {
            int id = RequestInt("id");
            string name = Request.Form["name"];
            int flowID = RequestInt("flow");
            var usersID = Request.Form["users[]"].Split(',').Select(o => o.AsInt());

            IList<User> users = UserService.GetUserList(usersID);


            WorkFlow w = WorkFlowService.GetById(flowID);

            if (id < 0)
            {
                WorkStep step = new WorkStep();
                step.Name = name;
                step.WorkFlow = w;
                step.Users = users;

                WorkStepService.Create(step);
            }
            else
            {
                WorkStep step = WorkStepService.GetById(id);
                step.Name = name;
                step.WorkFlow = w;
                step.Users = users;
                WorkStepService.Update(step);
            }

            return Json(new { sucess = true, Text = "Save Suceess!" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUsers(int id)
        {
            var users = UserService.GetUserList();

            foreach (var workstep in WorkStepService.GetAll())
            {
                if (workstep.Id != id)
                {
                    foreach (var user in workstep.Users)
                    {
                        var existUser = users.FirstOrDefault(o => o.Id == user.Id);
                        if (existUser != null)
                        {
                            users.Remove(existUser);
                        }
                    }
                }
            }

            var json = users.Select(o => new { id = o.Id, Name = o.UserName }).ToArray();
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);

        }
    }
}
