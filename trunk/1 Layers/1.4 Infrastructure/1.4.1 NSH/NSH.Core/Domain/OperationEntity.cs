using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    public class OperationEntity<T>
    {
        public OperationEntity()
        {
        }

        public OperationEntity(T entity, OperationType type)
        {
            Entity = entity;
            Type = type;
        }

        public T Entity { get; set; }

        public OperationType Type { get; set; }
    }
}
