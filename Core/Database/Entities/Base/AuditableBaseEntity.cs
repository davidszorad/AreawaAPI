using System;

namespace Core.Database.Entities
{
    internal class AuditableBaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
