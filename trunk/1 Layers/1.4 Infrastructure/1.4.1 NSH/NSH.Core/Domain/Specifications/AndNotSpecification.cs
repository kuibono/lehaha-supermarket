using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            var bodyNot = Expression.Not(Right.GetExpression().Body);
            var body = Expression.And(Left.GetExpression().Body, bodyNot);
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
        #endregion
    }
}
