using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NSH.Core.Domain.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor

        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods

        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.AndAlso(Left.GetExpression().Body, Right.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
        }
        #endregion
    }

}
