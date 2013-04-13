using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Authorization.Domain;
namespace TEWorkFlow.Domain.WorkFlow
{
    public class WorkStep : EntityBase, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public virtual IList<CheckPoint> CheckPoints { get; set; }

        public virtual WorkFlow WorkFlow { get; set; }

        public virtual IList<User> Users { get; set; }

        protected override void Validate()
        {
        }
    }
}
