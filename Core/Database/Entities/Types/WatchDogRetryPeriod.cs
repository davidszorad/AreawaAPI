using System.Collections.Generic;

namespace Core.Database.Entities;

public class WatchDogRetryPeriod
{
    public Domain.Enums.WatchDogRetryPeriod WebWatchDogRetryPeriodId { get; set; }
    public string Name { get; set; }

    public ICollection<WatchDog> ChangeTrackers { get; set; }
}