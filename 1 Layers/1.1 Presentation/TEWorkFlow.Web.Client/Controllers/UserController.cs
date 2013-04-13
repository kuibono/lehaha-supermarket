using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using NSH.Authorization.Domain;
using TEWorkFlow.Web.Client.Resources;
using NSH.Authorization.Service;
using TEWorkFlow.Web.Client.Models;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class UserController : Controller
    {
        private const int RootModuleId = 6;

        public IUserService UserService { get; set; }

        public IGroupService GroupService { get; set; }

        public IModuleService ModuleService { get; set; }

        public IRoleService RoleService { get; set; }

        //
        // GET: /User/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            User user;
            if (id.HasValue && id.Value > 0)
            {
                user = UserService.GetById(id.Value);
            }
            else
            {
                user = new User();
            }
            return View(user);
        }
        
        public ActionResult GetUserList()
        {
            DataGridPage dPage = new DataGridPage();
            string loginName = Request["loginname"];
            string userName = Request["username"];
            var list = UserService.GetUserList(loginName, userName, dPage, dPage)
                .Select(o =>
                new
                {
                    Id = o.Id,
                    LoginName = o.LoginName,
                    UserName = o.UserName,
                    Email = o.Email,
                    Group = String.Join(",", o.GroupList.Select(g => g.GroupName).ToArray())
                }).ToList();
            return Json(new { total = dPage.RecordCount, rows = list },JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGroupList(int? id)
        {
            Group group = GroupService.GetById(1);
            User user = id.HasValue ? UserService.GetById(id.Value) : null;
            var json = JsonTreeModel.Convert(new Group[] { group }, (g, treeModel) =>
            {
                treeModel.id = g.Id;
                treeModel.text = g.GroupName;
                if (user != null && user.GroupList != null && user.GroupList.Any(o => o.Id == g.Id))
                {
                    treeModel.@checked = true;
                }
            });
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleList(int? id)
        {
            Module module = ModuleService.GetById(RootModuleId);
            User user = id.HasValue ? UserService.GetById(id.Value) : null;
            var json = JsonTreeModel.Convert(module.Children, (m, treeModel) =>
            {
                treeModel.id = m.Id;
                treeModel.text = m.ModuleName;
                if (user != null && user.ModuleList != null && user.ModuleList.Any(o => o.Id == m.Id))
                {
                    treeModel.@checked = true;
                }
            }, o => o.IsEnable);
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleList()
        {
            var list = RoleService.GetAll();
            var json = list.Select(o => new { Id = o.Id, RoleName = o.RoleName });
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUser(User entity)
        {
            if (entity == null)
            {
                return Json(new { Success = false, Message = Global.SaveFail });
            }
            User user = entity.Id > 0 ? UserService.GetById(entity.Id) : new User();

            user.UserName = entity.UserName;
            user.Email = entity.Email;
            string groupIds = Request["group"];
            user.GroupList = new List<Group>();
            if (!String.IsNullOrWhiteSpace(groupIds))
            {
                foreach (var strId in groupIds.Split(','))
                {
                    int id;
                    if (int.TryParse(strId, out id))
                    {
                        user.GroupList.Add(new Group() { Id = id });
                    }
                }
            }

            string moduleIds = Request["module"];
            user.ModuleList = new List<Module>();
            if (!String.IsNullOrWhiteSpace(moduleIds))
            {
                foreach (var strId in moduleIds.Split(','))
                {
                    int id;
                    if (int.TryParse(strId, out id))
                    {
                        user.ModuleList.Add(new Module() { Id = id });
                    }
                }
            }

            string strRoleId = Request["Role"];
            int roleId;
            if (int.TryParse(strRoleId, out roleId))
            {
                user.Role = new Role() { Id = roleId };
            }

            if (user.Id > 0)
            {
                UserService.Update(user);
            }
            else
            {
                user.LoginName = entity.LoginName;
                user.Password = entity.LoginName.ToMD5();
                UserService.Create(user);
            }

            return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html");
        }

        [HttpPost]
        public ActionResult EditPassword(int id, string newpass)
        {
            JsonMessage result;
            if (id <= 0)
            {
                result = new JsonMessage(false, Global.WrongUser);
            }
            if (String.IsNullOrWhiteSpace(newpass))
            {
                result = new JsonMessage(false, Global.PasswordIsRequired);
            }
            try
            {

                var user = this.UserService.GetById(id);
                if (user != null)
                {
                    user.Password = newpass.ToMD5();
                    UserService.Update(user);
                    result = new JsonMessage(true, Global.SaveSuccess);
                }
                else
                {
                    result = new JsonMessage(false, Global.WrongUser);
                }
            }
            catch (Exception ex)
            {
                result = new JsonMessage(false, Global.SaveFail);
            }
            return Json(result, "text/html");
        }

        [HttpPost]
        public ActionResult ModifyPassword(string newPassword, string confirmNewPassword)
        {
            JsonMessage result;
            int id = Session[AuthorizeSettings.SessionUserID].ToString().AsInt();
            if (id <= 0)
            {
                result = new JsonMessage(false, Global.WrongUser);
            }
            if (String.IsNullOrWhiteSpace(newPassword)
                || String.IsNullOrWhiteSpace(confirmNewPassword))
            {
                result = new JsonMessage(false, Global.PasswordIsRequired);
            }
            if (newPassword != confirmNewPassword)
            {
                result = new JsonMessage(false, Global.PasswordIsRequired);
            }
            try
            {

                var user = this.UserService.GetById(id);
                if (user != null)
                {
                    user.Password = newPassword.ToMD5();
                    UserService.Update(user);
                    result = new JsonMessage(true, Global.SaveSuccess);
                }
                else
                {
                    result = new JsonMessage(false, Global.WrongUser);
                }
            }
            catch (Exception ex)
            {
                result = new JsonMessage(false, Global.SaveFail);
            }
            return Json(result, "text/html");
        }
    }
}
