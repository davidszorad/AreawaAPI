using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class RetryPeriodConfiguration : ConfigurationBase, IEntityTypeConfiguration<RetryPeriod>
{
    public void Configure(EntityTypeBuilder<RetryPeriod> builder)
    {
        builder.ToTable(nameof(RetryPeriod), SchemaType);

        builder.HasKey(x => x.RetryPeriodId);
        builder.Property( x => x.RetryPeriodId).ValueGeneratedNever();

        builder.Property(a => a.Name).HasMaxLength(255).IsRequired();

        builder
            .HasMany(x => x.WatchDogs)
            .WithOne()
            .HasForeignKey(x => x.RetryPeriodId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}