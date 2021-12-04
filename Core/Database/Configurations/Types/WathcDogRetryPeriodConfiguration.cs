using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class WathcDogRetryPeriodConfiguration : ConfigurationBase, IEntityTypeConfiguration<WatchDogRetryPeriod>
{
    public void Configure(EntityTypeBuilder<WatchDogRetryPeriod> builder)
    {
        builder.ToTable(nameof(WatchDogRetryPeriod), SchemaType);

        builder.HasKey(x => x.WebWatchDogRetryPeriodId);
        builder.Property( x => x.WebWatchDogRetryPeriodId).ValueGeneratedNever();

        builder.Property(a => a.Name).HasMaxLength(255).IsRequired();

        builder
            .HasMany(x => x.ChangeTrackers)
            .WithOne()
            .HasForeignKey(x => x.WatchDogId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}