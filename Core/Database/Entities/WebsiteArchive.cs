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

        public int ArchiveTypeId { get; set; }
        public ArchiveType ArchiveType { get; set; }

        public int EntityStatusId { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
