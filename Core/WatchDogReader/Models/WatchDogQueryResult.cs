using System;
using Domain.Enums;

namespace Core.WatchDogReader;

public class WatchDogQueryResult : BaseQueryResult
{
    public Guid PublicId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public long ScanCount { get; set; }
    public Status EntityStatusId { get; set; }
    public RetryPeriod RetryPeriodId { get; set; }
}