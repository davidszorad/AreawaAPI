namespace Core.WatchDogReader;

internal static class WatchDogExtensions
{
    public static WatchDogQueryResult Map(this Database.Entities.WatchDog entity)
    {
        return new WatchDogQueryResult
        {
            PublicId = entity.PublicId,
            Name = entity.Name,
            Url = entity.Url,
            StartSelector = entity.StartSelector,
            EndSelector = entity.EndSelector,
            IsActive = entity.IsActive,
            ScanCount = entity.ScanCount,
            Status = entity.EntityStatusId,
            RetryPeriod = entity.RetryPeriodId,
            Updated = entity.UpdatedOn,
            Created = entity.CreatedOn
        };
    }
}