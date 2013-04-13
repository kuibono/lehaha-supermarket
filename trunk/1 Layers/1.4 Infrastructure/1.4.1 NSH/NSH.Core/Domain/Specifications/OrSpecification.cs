using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor

        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.OrElse(Left.GetExpression().Body, Right.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
        #endregion
    }
}
