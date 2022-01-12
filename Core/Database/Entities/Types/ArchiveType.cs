using System.Collections.Generic;

namespace Core.Database.Entities;

public class ArchiveType
{
    public Domain.Enums.ArchiveType ArchiveTypeId { get; set; }
    public string Name { get; set; }

    public ICollection<WebsiteArchive> WebsiteArchives { get; set; }
}