using System;
using Domain.Enums;

namespace Core.WatchDogReader;

public class FilterQuery : IQueryObject
{
    internal FilterQuery()
    {
    }

    public Guid? UserPublicId { get; set; }
    public Guid? PublicId { get; internal set; }
    public string ShortId { get; internal set; }
    public Status? EntityStatus { get; set; }
    public bool? IsActive { get; set; }

    public SortBy SortBy { get; internal set; }
    public bool IsSortDescending { get; internal set; }
    public int Page { get; internal set; }
    public int PageSize { get; internal set; }
}