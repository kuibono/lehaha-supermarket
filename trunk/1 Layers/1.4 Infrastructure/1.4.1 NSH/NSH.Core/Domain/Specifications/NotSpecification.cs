using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        #region Private Fields
        private ISpecification<T> spec;
        #endregion

        #region Ctor

        public NotSpecification(ISpecification<T> specification)
        {
            this.spec = specification;
        }
        #endregion

        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.Not(this.spec.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this.spec.GetExpression().Parameters);
        }
        #endregion
    }
}
