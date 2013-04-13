using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.WorkFlow
{
    /// <summary>
    /// 发票
    /// </summary>
    public class Invoice : EntityBase, IAggregateRoot
    {
        public virtual WorkStep WorkStep { get; set; }

        public virtual string Title { get; set; }

        public virtual IList<InvoicePoint> Points { get; set; }

        protected override void Validate()
        {
        }
    }
}
