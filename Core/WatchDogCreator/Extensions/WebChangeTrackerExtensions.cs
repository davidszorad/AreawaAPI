namespace Core.WatchDogCreator;

internal static class WebChangeTrackerExtensions
{
    public static Database.Entities.WatchDog Map(this CreateWatchDogCommand command)
    {
        return new Database.Entities.WatchDog
        {
            Name = command.Name,
            Url = command.Url,
            StartSelector = command.StartSelector,
            EndSelector = command.EndSelector,
            RetryPeriodId = command.RetryPeriod
        };
    }
}