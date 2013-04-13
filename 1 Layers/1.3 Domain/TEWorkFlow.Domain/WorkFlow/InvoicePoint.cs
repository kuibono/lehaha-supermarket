using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Domain.WorkFlow
{
    public class InvoicePoint : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// Checkpoint
        /// </summary>
        public virtual CheckPoint CheckPoint { get; set; }

        /// <summary>
        /// 发票
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// 是否Check通过
        /// </summary>
        public virtual bool IsChecked { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public virtual string Remark { get; set; }

        public virtual User UpdateUser { get; set; }

        protected override void Validate()
        {
        }
    }
}
