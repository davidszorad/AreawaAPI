using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations
{
    internal class WebsiteArchiveConfiguration : ConfigurationBase, IEntityTypeConfiguration<WebsiteArchive>
    {
        public void Configure(EntityTypeBuilder<WebsiteArchive> builder)
        {
            builder.ToTable(nameof(WebsiteArchive), Schema);

            builder.HasKey(x => x.WebsiteArchiveId);
            builder.HasIndex(b => b.PublicId).IsUnique();
            builder.HasIndex(b => b.ShortId).IsUnique();
            
            builder.Property(a => a.PublicId).IsRequired();
            builder.Property(a => a.ShortId).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(255).IsRequired();
            builder.Property(a => a.SourceUrl).IsRequired();
            builder.Property(c => c.ArchiveTypeId).IsRequired().HasDefaultValue(Entities.Enums.ArchiveType.Pdf);
            builder.Property(c => c.EntityStatusId).IsRequired().HasDefaultValue(Entities.Enums.EntityStatus.Error);

            builder
                .HasOne(x => x.ArchiveType)
                .WithMany(x => x.WebsiteArchives)
                .HasForeignKey(x => x.ArchiveTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.EntityStatus)
                .WithMany(x => x.WebsiteArchives)
                .HasForeignKey(x => x.EntityStatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
