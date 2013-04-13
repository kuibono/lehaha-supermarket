using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Web.Client.Common;
using TEWorkFlow.Application.Service.WorkFlow;
using TEWorkFlow.Domain.WorkFlow;
using TEWorkFlow.Web.Client.Models;
using TEWorkFlow.Web.Client.Resources;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class CaseController : Controller
    {
        public ICaseService CaseService { get; set; }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetCaseList()
        {
            var list = CaseService.GetAll()
                .Select(o => new
                {
                    Id = o.Id,
                    Name = o.Name,
                    InvoiceCount = o.Invoices.Count
                }).ToArray();
            return Json(list, "text/html");
        }

        [HttpPost]
        public ActionResult Edit(Case entity)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                //检查名称
                if (String.IsNullOrWhiteSpace(entity.Name))
                {
                    result.Success = false;
                    result.Message = Global.SaveFail;
                }
                Case upEntity;
                //id有值则更新
                if (entity.Id > 0)
                {
                    upEntity = CaseService.GetById(entity.Id);
                    upEntity.Name = entity.Name;
                    CaseService.Update(upEntity);
                    result.Success = true;
                    result.Message = Global.SaveSuccess;
                }
                else
                {
                    upEntity = new Case();
                    upEntity.Name = entity.Name;
                    CaseService.Create(upEntity);
                    result.Success = true;
                    result.Message = Global.SaveSuccess;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = Global.SaveFail;
            }
            return Json(result, "text/html");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                var upEntity = CaseService.GetById(id);
                if (upEntity == null)
                {
                    result.Success = false;
                    result.Message = Global.DeleteFail;
                }
                else
                {
                    CaseService.Delete(upEntity);
                    result.Success = true;
                    result.Message = Global.SaveSuccess;
                }
            }
            catch
            {
                result.Success = false;
                result.Message = Global.DeleteFail;
            }
            return Json(result, "text/html");
        }

    }
}
