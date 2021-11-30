using Core.WebsiteArchiveReader;
using Domain.Enums;

namespace Core.Reader
{
    internal static class WebsiteArchiveExtensions
    {
        public static GetArchivedWebsiteQuery Map(this Database.Entities.WebsiteArchive entity)
        {
            return new GetArchivedWebsiteQuery
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
}
