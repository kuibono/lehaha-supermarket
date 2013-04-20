using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Category
{
    /// <summary>
    /// 政治面貌
    /// </summary>
    public class EmPaPolitics : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string Name { get; set; }

        protected override void Validate()
        {
            
        }
    }
}
