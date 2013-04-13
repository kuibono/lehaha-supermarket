using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NSH.Core.Domain;

namespace NSH.Authorization.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual int Level { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public virtual string Remark { get; set; }

        protected override void Validate()
        {
        }
    }
}
