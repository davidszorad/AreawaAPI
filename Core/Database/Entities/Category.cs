using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class Category : AuditableBaseEntity
    {
        public long CategoryId { get; set; }
        public Guid PublicId { get; set; }
        public string Name { get; set; }

        public long? CategoryGroupId { get; set; }
        public CategoryGroup CategoryGroup { get; set; }

        public ICollection<WebsiteArchiveCategory> WebsiteArchiveCategories { get; set; }
    }
}