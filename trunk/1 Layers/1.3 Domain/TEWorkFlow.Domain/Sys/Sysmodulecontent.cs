using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Sys
{
    public class Sysmodulecontent : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string Windowname { get; set; }
        public virtual string Openwindowname { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Url { get; set; }
        public virtual string ParentId { get; set; }
        public virtual int Index { get; set; }

        public virtual bool EmployeeVisible { get; set; }

        public virtual bool SupplierVisible { get; set; }

        public virtual bool OpenInNewWindow { get; set; }

        protected override void Validate()
        {
        }
    }
}
