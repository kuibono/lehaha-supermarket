using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public static class Extend
    {
        public static SearchResult<T> ToSearchResult<T>(this IList<T> entitys,int total)
        {
            SearchResult<T> result=new SearchResult<T>();
            result.total = total;
            result.data = entitys;
            return result;
        }
    }
}
