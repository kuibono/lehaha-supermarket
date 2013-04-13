using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain.Specifications
{
    public interface ISpecificationParser<TCriteria>
    {

        TCriteria Parse<T>(ISpecification<T> specification);
    }
}
