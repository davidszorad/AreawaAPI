using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core.Database.Configurations;

namespace Core.Database;

public class AreawaDbContext : DbContext
{
    public AreawaDbContext(DbContextOptions<AreawaDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<WebsiteArchive> WebsiteArchive { get; set; }
    public DbSet<ArchiveType> ArchiveType { get; set; }
    public DbSet<EntityStatus> EntityStatus { get; set; }
    public DbSet<RetryPeriod> WatchDogRetryPeriod { get; set; }
    public DbSet<Entities.WatchDog> WatchDog { get; set; }
    public DbSet<ApiUser> ApiUser { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<CategoryGroup> CategoryGroup { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ConfigurationBase.SchemaAreawa);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}