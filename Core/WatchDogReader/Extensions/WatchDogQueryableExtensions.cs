using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.WatchDogReader;

internal static class WatchDogQueryableExtensions
{
    public static IQueryable<WatchDog> AddCustomFilters(this IQueryable<WatchDog> query, FilterQuery filterQuery, bool isPagingAndOrderingEnabled = true)
    {
        if (filterQuery == null)
        {
            return query;
        }

        query = query.AddUserFilter(filterQuery);
        query = query.AddIdFilter(filterQuery);
        query = query.AddStatusFilter(filterQuery);
        
        if (isPagingAndOrderingEnabled)
        {
            query = query.ApplyOrdering(filterQuery, GetColumnsMap());
            query = query.ApplyPaging(filterQuery);
        }
        
        if (!filterQuery.IncludeInactive)
        {
            query = query.Where(x => x.IsActive == true);
        }

        return query;
    }
    
    private static IQueryable<WatchDog> AddUserFilter(this IQueryable<WatchDog> query, FilterQuery filterQuery)
    {
        if (filterQuery.UserPublicId.HasValue)
        {
            query = query
                .Include(x => x.ApiUser)
                .Where(x => x.ApiUser.PublicId == filterQuery.UserPublicId.Value);
        }

        return query;
    }
    
    private static IQueryable<WatchDog> AddIdFilter(this IQueryable<WatchDog> query, FilterQuery filterQuery)
    {
        if (filterQuery.PublicId.HasValue)
        {
            query = query.Where(x => x.PublicId == filterQuery.PublicId.Value);
        }

        return query;
    }
    
    private static IQueryable<WatchDog> AddStatusFilter(this IQueryable<WatchDog> query, FilterQuery filterQuery)
    {
        if (filterQuery.EntityStatus.HasValue)
        {
            query = query.Where(x => x.EntityStatusId == filterQuery.EntityStatus.Value);
        }

        return query;
    }
    
    private static Dictionary<int, Expression<Func<WatchDog, object>>> GetColumnsMap()
    {
        return new Dictionary<int, Expression<Func<WatchDog, object>>>()
        {
            [(int)SortBy.Default] = p => p.Name,
            [(int)SortBy.Name] = p => p.Name,
            [(int)SortBy.Date] = p => p.UpdatedOn,
            [(int)SortBy.Status] = p => p.EntityStatusId
        };
    }
}