using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Authorization.Domain;
namespace TEWorkFlow.Domain.WorkFlow
{
    /// <summary>
    /// Check point if a Invoice
    /// </summary>
    public class CheckPoint : EntityBase, IAggregateRoot
    {
        public virtual User CheckUser { get; set; }

        public virtual string ItemName { get; set; }

        public virtual WorkStep Step { get; set; }

        public virtual bool IsKey { get; set; }

        public virtual int OrderIndex { get; set; }

        /// <summary>
        /// 类型(主要用于区分日期和其他数据类型)
        /// </summary>
        public virtual CheckPointType CheckPointType { get; set; }

        /// <summary>
        /// 引用此CheckPoint的InvoicePoint列表
        /// </summary>
        public virtual IList<InvoicePoint> InvoicePoints { get; set; }

        protected override void Validate()
        {
        }
    }

    public enum CheckPointType
    {
        General = 1,
        Date = 2
    }
}
