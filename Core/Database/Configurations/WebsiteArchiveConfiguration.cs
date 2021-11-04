using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations
{
    internal class WebsiteArchiveConfiguration : IEntityTypeConfiguration<WebsiteArchive>
    {
        public void Configure(EntityTypeBuilder<WebsiteArchive> builder)
        {
            builder.ToTable(nameof(WebsiteArchive));

            builder.HasKey(x => x.WebsiteArchiveId);
            builder.HasIndex(b => b.PublicId).IsUnique();
            builder.HasIndex(b => b.ShortId).IsUnique();

            builder.Property(a => a.PublicId).IsRequired();
            builder.Property(a => a.ShortId).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(255).IsRequired();
            builder.Property(a => a.SourceUrl).IsRequired();
            builder.Property(c => c.ArchiveTypeId).IsRequired().HasDefaultValue(Domain.Enums.ArchiveType.Pdf);
            builder.Property(c => c.EntityStatusId).IsRequired().HasDefaultValue(Entities.Enums.EntityStatus.Error);
        }
    }
}
