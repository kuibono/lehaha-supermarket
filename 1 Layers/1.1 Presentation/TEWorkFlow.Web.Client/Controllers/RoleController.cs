using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEWorkFlow.Web.Client.Common;
using NSH.Authorization.Service;
using TEWorkFlow.Web.Client.Models;
using TEWorkFlow.Web.Client.Resources;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class RoleController : Controller
    {
        public IRoleService RoleService { get; set; }
        //
        // GET: /Role/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetRoleList()
        {
            var list = RoleService.GetAll();
            return Json(list, "text/html");
        }

        [HttpPost]
        public ActionResult Edit(Role entity)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                //检查名称
                if (String.IsNullOrWhiteSpace(entity.RoleName))
                {
                    result.Success = false;
                    result.Message = Global.SaveFail;
                }
                Role role;
                //id有值则更新
                if (entity.Id > 0)
                {
                    role = RoleService.GetById(entity.Id);
                    role.RoleName = entity.RoleName;
                    role.Level = entity.Level;
                    role.Remark = entity.Remark;
                    RoleService.Update(role);
                    result.Success = true;
                    result.Message = Global.SaveSuccess;
                }
                else
                {
                    role = new Role();
                    role.RoleName = entity.RoleName;
                    role.Level = entity.Level;
                    role.Remark = entity.Remark;
                    RoleService.Create(role);
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
                var group = RoleService.GetById(id);
                if (group == null)
                {
                    result.Success = false;
                    result.Message = Global.DeleteFail;
                }
                else
                {
                    RoleService.Delete(group);
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
