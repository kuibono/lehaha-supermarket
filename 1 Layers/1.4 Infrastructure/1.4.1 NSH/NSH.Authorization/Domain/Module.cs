using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace NSH.Authorization.Domain
{
    [Serializable]
    public class Module : EntityBase, IAggregateRoot, ITreeEntity<Module>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 是否根模块
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// 父级模块
        /// </summary>
        public virtual Module Parent { get; set; }

        /// <summary>
        /// 子模块列表
        /// </summary>
        public virtual IList<Module> Children { get; set; }

        /// <summary>
        /// 路径（类全命名空间、链接）
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// 是否子窗体
        /// </summary>
        public virtual bool IsMDI { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool IsEnable { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public virtual IList<User> UserList { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
