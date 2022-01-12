using System;

namespace Core.Database.Entities;

public class AuditableBaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}