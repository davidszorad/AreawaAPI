﻿using System;

namespace Core.Database.Entities
{
    public class WebsiteArchive : AuditableBaseEntity
    {
        public long WebsiteArchiveId { get; set; }
        public Guid PublicId { get; set; }
        public string ShortId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public string ArchiveUrl { get; set; }
        public Domain.Enums.ArchiveType ArchiveTypeId { get; set; }
        public Domain.Enums.Status EntityStatusId { get; set; }
    }
}
