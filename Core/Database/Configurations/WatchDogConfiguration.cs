using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class WatchDogConfiguration : IEntityTypeConfiguration<Entities.WatchDog>
{
    public void Configure(EntityTypeBuilder<Entities.WatchDog> builder)
    {
        builder.ToTable(nameof(Entities.WatchDog));

        builder.HasKey(x => x.WatchDogId);
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.Property(x => x.PublicId).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.StartSelector).HasMaxLength(250).IsRequired();
        builder.Property(x => x.EndSelector).HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.EntityStatusId).IsRequired().HasDefaultValue(Domain.Enums.Status.Error);
        builder.Property(x => x.RetryPeriodId).IsRequired().HasDefaultValue(Domain.Enums.RetryPeriod.OneWeek);
        builder.Property(x => x.ApiUserId).IsRequired();
    }
}