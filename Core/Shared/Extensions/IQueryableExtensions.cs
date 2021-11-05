using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Shared
{
    internal static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject filterQuery)
        {
            int page = filterQuery.Page;
            int pageSize = filterQuery.PageSize;

            if (page <= 0)
                page = PagingConstants.DEFAULT_PAGE;

            if (pageSize <= 0)
                pageSize = PagingConstants.DEFAULT_PAGE_SIZE;

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject filterQuery, Dictionary<int, Expression<Func<T, object>>> columnsMap)
        {
            if (!columnsMap.ContainsKey((int)filterQuery.SortBy))
            {
                return query;
            }

            return filterQuery.IsSortDescending
                ? query.OrderByDescending(columnsMap[(int)filterQuery.SortBy])
                : query.OrderBy(columnsMap[(int)filterQuery.SortBy]);
        }
    }
}
