using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.WorkFlow
{
    public class Case : EntityBase, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public virtual CaseStatus Status { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }

        protected override void Validate()
        {
        }
    }
}
