using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class WebsiteArchiveCategoryConfiguration : IEntityTypeConfiguration<WebsiteArchiveCategory>
{
    public void Configure(EntityTypeBuilder<WebsiteArchiveCategory> builder)
    {
        builder.ToTable(nameof(WebsiteArchiveCategory));

        builder.HasKey(x => new { x.WebsiteArchiveId, x.CategoryId });

        builder
            .HasOne(x => x.WebsiteArchive)
            .WithMany(x => x.WebsiteArchiveCategories)
            .HasForeignKey(x => x.WebsiteArchiveId)
            .IsRequired();

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.WebsiteArchiveCategories)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired();
    }
}