using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Application.Service.WorkFlow;
using NSH.Authorization.Service;
using TEWorkFlow.Domain.WorkFlow;
using TEWorkFlow.Web.Client.Common;
using TEWorkFlow.Web.Client.Models;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class CheckPointController : Controller
    {
        #region Service property
        public ICheckPointService CheckPointService { get; set; }

        public IUserService UserService { get; set; }

        public IWorkStepService WorkStepService { get; set; } 
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? workstepId)
        {
            ViewBag.WorkStepId = workstepId.HasValue ? workstepId.Value : 0;
            return View();
        }

        public ActionResult Delete(int id)
        {
            var checkPoint = CheckPointService.GetById(id);
            if (checkPoint.InvoicePoints != null && checkPoint.InvoicePoints.Count > 0)
            {
                return Json(new { success = false, Text = Resources.Global.CheckPonitDeleteInUsed }, "text/plain", JsonRequestBehavior.AllowGet);
            }
            CheckPointService.Delete(checkPoint);
            return Json(new { success = true, Text = Resources.Global.DeleteSuccess }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCheckPointList(int workstepId)
        {
            var list = CheckPointService.GetListByWorkstep(new WorkStep() { Id = workstepId });
            var json = list.Select(p => new
            {
                id = p.Id,
                itemname = p.ItemName,
                //username=p.CheckUser.UserName,
                //userid=p.CheckUser.Id,
                status = p.Step.Id,
                statusText = p.IsKey ? "Key" : p.Step.Name,
                orderindex = p.OrderIndex,
                Type = p.CheckPointType.ToString(),
                TypeID = (int)p.CheckPointType,
                iskey = p.IsKey
            });
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers(bool addEmpty = false)
        {
            var list = UserService.GetUserList();

            var json = list.Select(o => new { Id = o.Id.ToString(), UserName = o.UserName }).ToList();
            if (addEmpty)
            {
                json.Insert(0, new { Id = "", UserName = Resources.Global.PleaseSelect });
            }
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSteps(bool addEmpty = false)
        {
            var list = WorkStepService.GetAll();
            var json = list.Select(p => new { Id = p.Id.ToString(), Name = p.Name }).ToList();
            if (addEmpty)
            {
                json.Insert(0, new { Id = "", Name = Resources.Global.PleaseSelect });
            }
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCheckPointTypes()
        {
            var list = new List<object>();
            foreach (var item in Enum.GetValues(typeof(CheckPointType)))
            {
                string s = Enum.GetName(typeof(CheckPointType), item);
                list.Add(new { Id = ((int)item).ToString(), Name = s });
            }
            //var list = WorkStepService.GetAll().Select(p => new
            //{
            //    p.Id,
            //    p.Name
            //});
            return Json(list, "text/plain", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 行编辑保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemName"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCheckPoint(int id, string itemName, int typeid)
        {
            JsonMessage jsMsg = new JsonMessage();

            if (id <= 0)
            {
                return Json(jsMsg.AsFail("CheckPoint not found!"), JsonMessage.ContentType);
            }

            if (String.IsNullOrWhiteSpace(itemName))
            {
                return Json(jsMsg.AsFail("Name is required!"), JsonMessage.ContentType);
            }

            var checkPoint = CheckPointService.GetById(id);

            if (checkPoint == null)
            {
                return Json(jsMsg.AsFail("CheckPoint not found!"), JsonMessage.ContentType);
            }

            checkPoint.ItemName = itemName;
            if (typeid > 0)
            {
                checkPoint.CheckPointType = (CheckPointType)typeid;
            }

            CheckPointService.Update(checkPoint);

            return Json(jsMsg.AsSuccess("Update Successed!"), JsonMessage.ContentType);
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="stepid"></param>
        /// <param name="iskey"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public ActionResult SaveCheckPoint(string itemName, int stepid, bool iskey, int typeid)
        {
            JsonMessage jsMsg = new JsonMessage();

            if (String.IsNullOrWhiteSpace(itemName))
            {
                return Json(jsMsg.AsFail("Name is required!"), JsonMessage.ContentType);
            }

            if (stepid <= 0)
            {
                return Json(jsMsg.AsFail("Workstep is required!"), JsonMessage.ContentType);
            }

            CheckPoint checkPoint = new CheckPoint();
            checkPoint.CheckUser = null;
            checkPoint.IsKey = iskey;
            checkPoint.ItemName = itemName;
            checkPoint.OrderIndex = 1;
            checkPoint.Step = new WorkStep() { Id = stepid };
            checkPoint.CheckPointType = (CheckPointType)typeid;

            CheckPointService.Create(checkPoint);

            return Json(jsMsg.AsSuccess("Add Successed!"), JsonMessage.ContentType);
        }
    }
}
