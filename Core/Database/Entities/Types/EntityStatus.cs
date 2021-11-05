using System.Collections.Generic;
using Domain.Enums;

namespace Core.Database.Entities
{
    public class EntityStatus
    {
        public Status EntityStatusId { get; set; }
        public string Name { get; set; }

        public ICollection<WebsiteArchive> WebsiteArchives { get; set; }

        public EntityStatus()
        {
            WebsiteArchives = new List<WebsiteArchive>();
        }
    }
}
