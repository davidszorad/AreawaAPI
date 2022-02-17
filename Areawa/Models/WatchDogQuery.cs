using System;
using Core.Shared;
using Domain.Enums;

namespace Areawa.Models;

public class WatchDogQuery
{
    public Guid? PublicId { get; set; }
    public bool? IncludeInactive { get; set; }
    public Status? Status { get; set; }

    public SortBy SortBy { get; set; }
    public bool IsSortDescending { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}