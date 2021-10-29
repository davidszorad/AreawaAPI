using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class ArchiveType
    {
        public int ArchiveTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<WebsiteArchive> WebsiteArchives { get; set; }

        public ArchiveType()
        {
            WebsiteArchives = new List<WebsiteArchive>();
        }
    }
}
