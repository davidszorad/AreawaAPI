using System;

namespace Core.Shared.Entities
{
    internal class AuditableBaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
