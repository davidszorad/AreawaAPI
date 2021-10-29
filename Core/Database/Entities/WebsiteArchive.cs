using System;

namespace Core.Database.Entities
{
    public class WebsiteArchive
    {
        public long WebsiteArchiveId { get; set; }
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        
        public Enums.ArchiveType ArchiveTypeId { get; set; }
        public ArchiveType ArchiveType { get; set; }

        public Enums.EntityStatus EntityStatusId { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
