using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NSH.Core.Domain;

namespace NSH.Authorization.Domain
{
    public class Group : EntityBase, IAggregateRoot, ITreeEntity<Group>
    {
        /// <summary>
        /// 组名称
        /// </summary>
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 是否系统用户组
        /// </summary>
        public virtual bool IsSystem { get; set; }

        /// <summary>
        /// 是否根用户组
        /// </summary>
        public virtual bool IsRoot { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual int Level { get; set; }

        /// <summary>
        /// 父级组
        /// </summary>
        public virtual Group Parent { get; set; }

        /// <summary>
        /// 子组列表
        /// </summary>
        public virtual IList<Group> Children { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public virtual IList<User> UserList { get; set; }


        protected override void Validate()
        {
            if (String.IsNullOrEmpty(GroupName))
            {
                this.AddBrokenRule("GroupName", "组名不能为空");
            }
        }
    }
}
