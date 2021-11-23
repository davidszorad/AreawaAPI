using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class CategoryGroup : AuditableBaseEntity
    {
        public long CategoryGroupId { get; set; }
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        
        public long ApiUserId { get; set; }
        public ApiUser ApiUser { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}