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
