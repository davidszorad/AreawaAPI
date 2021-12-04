using System.Collections.Generic;

namespace Core.Database.Entities;

public class RetryPeriod
{
    public Domain.Enums.RetryPeriod RetryPeriodId { get; set; }
    public string Name { get; set; }

    public ICollection<WatchDog> WatchDogs { get; set; }
}