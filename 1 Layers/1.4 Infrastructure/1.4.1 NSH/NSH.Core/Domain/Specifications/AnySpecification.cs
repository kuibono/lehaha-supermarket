﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    public sealed class AnySpecification<T> : Specification<T>
    {
        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            return o => true;
        }
        #endregion
    }
}
