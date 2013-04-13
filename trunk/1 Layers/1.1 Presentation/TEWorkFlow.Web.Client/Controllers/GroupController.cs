using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NSH.Authorization.Domain;
using NSH.Authorization.Service;
using TEWorkFlow.Web.Client.Models;
using TEWorkFlow.Web.Client.Resources;
using TEWorkFlow.Web.Client.Common;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class GroupController : Controller
    {
        public IGroupService GroupService { get; set; }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetGroupList(int? id)
        {
            Group group = GroupService.GetById(1);
            var json = JsonTreeModel.Convert(new Group[] { group }, (g, treeModel) =>
            {
                treeModel.id = g.Id;
                treeModel.text = g.GroupName;
                treeModel.count = GetUserCount(g);
                treeModel.remark = g.Remark;
                treeModel.issystem = g.IsSystem;
                if (g.Parent != null)
                {
                    treeModel.parentid = g.Parent.Id;
                }
            });
            return Json(json, "text/plain", JsonRequestBehavior.AllowGet);
        }

        private int GetUserCount(Group group)
        {
            int count = 0;
            count += group.UserList.Count;
            if (group.Children != null && group.Children.Count > 0)
            {
                foreach (var childGroup in group.Children)
                {
                    count += GetUserCount(childGroup);
                }
            }
            return count;
        }

        [HttpPost]
        public ActionResult Edit(int? id, int parentid, string text, string remark)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                //检查名称
                if (String.IsNullOrWhiteSpace(text))
                {
                    result.Success = false;
                    result.Message = Global.SaveFail;
                }
                Group group;
                //id有值则更新
                if (id.HasValue && id.Value > 0)
                {
                    group = GroupService.GetById(id.Value);
                    group.GroupName = text;
                    group.Remark = remark;
                    GroupService.Update(group);
                    result.Success = true;
                    result.Message = Global.SaveSuccess;
                }
                else
                {
                    group = new Group();
                    var parentGroup = GroupService.GetById(parentid);
                    if (parentGroup != null)
                    {
                        group.Parent = parentGroup;
                        group.Level = parentGroup.Level + 1;
                        group.IsSystem = false;
                        group.GroupName = text;
                        group.Remark = remark;
                        GroupService.Create(group);
                        result.Success = true;
                        result.Message = Global.SaveSuccess;
                    }
                    else //parent id 错误
                    {
                        result.Success = false;
                        result.Message = Global.SaveFail;
                    }
                }
            }
            catch(Exception ex)
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
                var group = GroupService.GetById(id);
                if(group == null)
                {
                    result.Success = false;
                    result.Message = Global.GroupNotExist;
                }
                else if (group.Children.Count > 0 || GetUserCount(group) > 0)
                {
                    result.Success = false;
                    result.Message = Global.UsersGroupOrSubgroup;
                }
                else
                {
                    GroupService.Delete(group);
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
