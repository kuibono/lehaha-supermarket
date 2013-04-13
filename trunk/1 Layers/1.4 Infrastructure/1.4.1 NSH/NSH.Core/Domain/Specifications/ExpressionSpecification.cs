using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    internal sealed class ExpressionSpecification<T> : Specification<T>
    {
        #region Private Fields
        private Expression<Func<T, bool>> expression;
        #endregion

        #region Ctor

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }
        #endregion

        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
        #endregion
    }
}
