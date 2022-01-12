﻿using System;
using Core.Shared;
using Domain.Enums;

namespace Core.WebsiteArchiveReader;

public class FilterQueryBuilder
{
    private readonly FilterQuery _filterQuery = new();
    private bool _isGettingAllItems = true;

    public FilterQueryBuilder SetUserPublicId(Guid value)
    {
        _filterQuery.UserPublicId = value;
        return this;
    }
        
    public FilterQueryBuilder SetPublicId(Guid value)
    {
        _filterQuery.PublicId = value;
        _isGettingAllItems = false;
        return this;
    }

    public FilterQueryBuilder SetShortId(string value)
    {
        _filterQuery.ShortId = value;
        _isGettingAllItems = false;
        return this;
    }

    public FilterQueryBuilder SetStatus(Status value)
    {
        _filterQuery.EntityStatus = value;

        return this;
    }

    public FilterQueryBuilder SetPaging(int page, int pageSize)
    {
        _filterQuery.Page = page;
        _filterQuery.PageSize = pageSize;
        return this;
    }

    public FilterQueryBuilder SetOrdering(SortBy sortBy, bool isSortDescending = false)
    {
        _filterQuery.SortBy = sortBy;
        _filterQuery.IsSortDescending = isSortDescending;
        return this;
    }

    public FilterQueryBuilder OnlyActive()
    {
        _filterQuery.IsActive = true;
        return this;
    }

    public FilterQuery Build()
    {
        if (!IsValid())
        {
            throw new ArgumentException(nameof(FilterQuery));
        }

        if (_filterQuery.Page == default && _filterQuery.PageSize == default)
        {
            SetPaging(PagingConstants.DEFAULT_PAGE, PagingConstants.DEFAULT_PAGE_SIZE);
        }

        if (_filterQuery.SortBy == default && _filterQuery.IsSortDescending == default)
        {
            SetOrdering(SortBy.Name, false);
        }

        return _filterQuery;
    }

    private bool IsValid()
    {
        if (!_filterQuery.UserPublicId.HasValue)
        {
            return false;
        }
            
        if (_filterQuery.PublicId.HasValue || !string.IsNullOrWhiteSpace(_filterQuery.ShortId) || _isGettingAllItems)
        {
            return true;
        }

        return false;
    }
}