using Core.Database.Entities;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Reader
{
    internal static class WebsiteArchiveQueryableExtensions
    {
        public static IQueryable<WebsiteArchive> AddCustomFilters(this IQueryable<WebsiteArchive> query, FilterQuery filterQuery, bool isPagingAndOrderingEnabled = true)
        {
            if (filterQuery == null)
            {
                return query;
            }

            query = query.AddIdFilter(filterQuery);

            if (isPagingAndOrderingEnabled)
            {
                query = query.ApplyOrdering(filterQuery, GetColumnsMap());
                query = query.ApplyPaging(filterQuery);
            }

            return query;
        }

        private static IQueryable<WebsiteArchive> AddIdFilter(this IQueryable<WebsiteArchive> query, FilterQuery filterQuery)
        {
            if (filterQuery.PublicId.HasValue)
            {
                query = query.Where(x => x.PublicId == filterQuery.PublicId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filterQuery.ShortId))
            {
                query = query.Where(x => x.ShortId == filterQuery.ShortId);
            }

            return query;
        }

        private static Dictionary<int, Expression<Func<WebsiteArchive, object>>> GetColumnsMap()
        {
            return new Dictionary<int, Expression<Func<WebsiteArchive, object>>>()
            {
                [(int)SortBy.Default] = p => p.Name,
                [(int)SortBy.Name] = p => p.Name,
                [(int)SortBy.Date] = p => p.UpdatedOn,
                [(int)SortBy.Status] = p => p.EntityStatusId
            };
        }
    }
}
