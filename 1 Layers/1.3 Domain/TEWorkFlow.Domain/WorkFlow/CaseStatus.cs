using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Domain.WorkFlow
{
    public class CaseStatus : EntityBase, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public virtual CaseStatus Next { get; set; }

        public virtual User User { get; set; }

        protected override void Validate()
        {
        }
    }
}
