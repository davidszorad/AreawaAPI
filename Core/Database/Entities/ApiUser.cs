using System;
using System.Collections.Generic;

namespace Core.Database.Entities;

public class ApiUser : AuditableBaseEntity
{
    public long ApiUserId { get; set; }
    public Guid PublicId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public bool IsPremium { get; set; }

    public ICollection<WebsiteArchive> WebsiteArchives { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<CategoryGroup> CategoryGroups { get; set; }
}