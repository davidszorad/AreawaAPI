using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class WebsiteArchiveConfiguration : IEntityTypeConfiguration<WebsiteArchive>
{
    public void Configure(EntityTypeBuilder<WebsiteArchive> builder)
    {
        builder.ToTable(nameof(WebsiteArchive));

        builder.HasKey(x => x.WebsiteArchiveId);
        builder.HasIndex(x => x.PublicId).IsUnique();
        builder.HasIndex(x => x.ShortId).IsUnique();

        builder.Property(x => x.PublicId).IsRequired();
        builder.Property(x => x.ShortId).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.SourceUrl).IsRequired();
        builder.Property(x => x.ArchiveTypeId).IsRequired().HasDefaultValue(Domain.Enums.ArchiveType.Pdf);
        builder.Property(x => x.EntityStatusId).IsRequired().HasDefaultValue(Domain.Enums.Status.Error);
        builder.Property(x => x.ApiUserId).IsRequired();
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
    }
}