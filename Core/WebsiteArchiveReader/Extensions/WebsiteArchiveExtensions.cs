using Domain.Enums;

namespace Core.WebsiteArchiveReader;

internal static class WebsiteArchiveExtensions
{
    public static WebsiteArchiveQueryResult Map(this Database.Entities.WebsiteArchive entity)
    {
        return new WebsiteArchiveQueryResult
        {
            PublicId = entity.PublicId,
            ShortId = entity.ShortId,
            Name = entity.Name,
            Description = entity.Description,
            Status = (Status)entity.EntityStatusId,
            ArchiveType = entity.ArchiveTypeId,
            SourceUrl = entity.SourceUrl,
            ArchiveUrl = entity.ArchiveUrl,
            Created = entity.CreatedOn,
            Updated = entity.UpdatedOn
        };
    }
}