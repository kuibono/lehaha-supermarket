using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain.Specifications
{
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        ISpecification<T> Left { get; }

        ISpecification<T> Right { get; }
    }
}
