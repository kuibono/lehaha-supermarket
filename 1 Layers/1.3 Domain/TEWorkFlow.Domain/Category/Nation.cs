using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Category
{
    public class Nation : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string Name { get; set; }
        protected override void Validate()
        {

        }
    }
}
