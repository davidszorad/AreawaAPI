using System;

namespace Core.Database.Entities;

public class ApiUser : AuditableBaseEntity
{
    public long ApiUserId { get; set; }
    public Guid PublicId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public Guid ApiKey { get; set; }
}