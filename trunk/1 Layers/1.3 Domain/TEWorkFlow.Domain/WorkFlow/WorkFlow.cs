using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NSH.Core.Domain;

namespace TEWorkFlow.Domain.WorkFlow
{
    public class WorkFlow : EntityBase, IAggregateRoot
    {
        public virtual bool IsDefault { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<WorkStep> Steps { get; set; }

        protected override void Validate()
        {
        }
    }
}
