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
            IsActive = entity.IsActive,
            ScanCount = entity.ScanCount,
            EntityStatusId = entity.EntityStatusId,
            RetryPeriodId = entity.RetryPeriodId,
            Updated = entity.UpdatedOn,
            Created = entity.CreatedOn
        };
    }
}