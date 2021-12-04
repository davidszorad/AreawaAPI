using System;

namespace Core.Database.Entities;

public class WatchDog : AuditableBaseEntity
{
    public long WatchDogId { get; set; }
    public Guid PublicId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string StartSelector { get; set; }
    public string EndSelector { get; set; }
    public string ContentHash { get; set; }
    public bool IsActive { get; set; }
    public Domain.Enums.Status EntityStatusId { get; set; }
    public Domain.Enums.RetryPeriod RetryPeriodId { get; set; }
    public long ApiUserId { get; set; }
    public ApiUser ApiUser { get; set; }
}