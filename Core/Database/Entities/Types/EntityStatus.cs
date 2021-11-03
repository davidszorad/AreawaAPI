using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class EntityStatus
    {
        public Enums.EntityStatus EntityStatusId { get; set; }
        public string Name { get; set; }

        public ICollection<WebsiteArchive> WebsiteArchives { get; set; }

        public EntityStatus()
        {
            WebsiteArchives = new List<WebsiteArchive>();
        }
    }
}
