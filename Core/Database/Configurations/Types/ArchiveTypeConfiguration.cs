using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class ArchiveTypeConfiguration : ConfigurationBase, IEntityTypeConfiguration<ArchiveType>
{
    public void Configure(EntityTypeBuilder<ArchiveType> builder)
    {
        builder.ToTable(nameof(ArchiveType), SchemaType);

        builder.HasKey(x => x.ArchiveTypeId);
        builder.Property( x => x.ArchiveTypeId).ValueGeneratedNever();

        builder.Property(a => a.Name).HasMaxLength(255).IsRequired();

        builder
            .HasMany(x => x.WebsiteArchives)
            .WithOne()
            .HasForeignKey(x => x.ArchiveTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}