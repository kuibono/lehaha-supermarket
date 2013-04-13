using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace NSH.Authorization.Domain
{
    public class User : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public virtual int UserState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string UserReMark { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 电子邮件密码
        /// </summary>
        public virtual string EmailPassword { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public virtual bool EnableEdit { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 组列表
        /// </summary>
        public virtual IList<Group> GroupList { get; set; }

        /// <summary>
        /// 模块列表
        /// </summary>
        public virtual IList<Module> ModuleList { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(LoginName))
            {
                this.AddBrokenRule("LoginName", "登录名不能为空");
            }
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                this.AddBrokenRule("Password", "密码不能为空或长度小于6");
            }
            if (string.IsNullOrEmpty(UserName))
            {
                this.AddBrokenRule("UserName", "用户名不能为空");
            }
        }
    }
}
