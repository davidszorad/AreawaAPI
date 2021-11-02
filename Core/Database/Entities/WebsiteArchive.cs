using System;

namespace Core.Database.Entities
{
    public class WebsiteArchive
    {
        public long WebsiteArchiveId { get; set; }
        public Guid PublicId { get; set; }
        public string ShortId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public string ArchiveUrl { get; set; }
        public Enums.ArchiveType ArchiveTypeId { get; set; }
        public Enums.EntityStatus EntityStatusId { get; set; }
    }
}
