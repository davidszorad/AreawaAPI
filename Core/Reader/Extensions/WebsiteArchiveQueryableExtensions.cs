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

            query = AddIdFilter(query, filterQuery);

            if (isPagingAndOrderingEnabled)
            {
                query = ApplyOrdering(query, filterQuery);
                query = ApplyPaging(query, filterQuery);
            }

            return query;
        }

        private static IQueryable<WebsiteArchive> AddIdFilter(IQueryable<WebsiteArchive> query, FilterQuery filterQuery)
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

        private static IQueryable<WebsiteArchive> ApplyPaging(IQueryable<WebsiteArchive> query, FilterQuery filterQuery)
        {
            if (filterQuery.Page <= 0)
                filterQuery.Page = PagingConstants.DEFAULT_PAGE;

            if (filterQuery.PageSize <= 0)
                filterQuery.PageSize = PagingConstants.DEFAULT_PAGE_SIZE;

            return query.Skip((filterQuery.Page - 1) * filterQuery.PageSize).Take(filterQuery.PageSize);
        }

        private static IQueryable<WebsiteArchive> ApplyOrdering(IQueryable<WebsiteArchive> query, FilterQuery filterQuery)
        {
            var columnsMap = new Dictionary<SortBy, Expression<Func<WebsiteArchive, object>>>()
            {
                [SortBy.Default] = p => p.Name,
                [SortBy.Name] = p => p.Name,
                [SortBy.Date] = p => p.UpdatedOn,
                [SortBy.Status] = p => p.EntityStatusId
            };

            return filterQuery.IsSortDescending
                ? query.OrderByDescending(columnsMap[filterQuery.SortBy])
                : query.OrderBy(columnsMap[filterQuery.SortBy]);
        }
    }
}
