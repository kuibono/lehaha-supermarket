using System;
using System.Collections.Generic;
using System.Linq;
using NSH.Core.Domain;

namespace NSH.Core.DataPage
{
    public static class PageQueryableExtensions
    {
        public static IQueryable<TEntity> Count<TEntity>(this IQueryable<TEntity> query, IDataPage datapage)
            where TEntity : EntityBase
        {
            datapage.RecordCount = query.Count();
            return query;
        }

        public static IQueryable<TEntity> CountAndPage<TEntity>(this IQueryable<TEntity> query, IDataPage datapage)
            where TEntity : EntityBase
        {
            datapage.RecordCount = query.Count();
            query = query.Skip(datapage.Skip).Take(datapage.PageSize);
            return query;
        }
    }
}
