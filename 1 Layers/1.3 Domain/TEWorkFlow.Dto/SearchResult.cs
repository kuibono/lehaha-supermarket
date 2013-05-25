using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class SearchResult<T>
    {
        public int total { get; set; }

        public IList<T> data { get; set; }

        public object Statistics { get; set; }

    }
}
