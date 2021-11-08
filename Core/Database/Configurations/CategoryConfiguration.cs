using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(x => x.CategoryId);
            builder.HasIndex(b => b.PublicId).IsUnique();

            builder.Property(a => a.PublicId).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(255).IsRequired();
        }
    }
    
    internal class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
    {
        public void Configure(EntityTypeBuilder<CategoryGroup> builder)
        {
            builder.ToTable(nameof(CategoryGroup));

            builder.HasKey(x => x.CategoryGroupId);
            builder.HasIndex(b => b.PublicId).IsUnique();

            builder.Property(a => a.PublicId).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(255).IsRequired();

            builder
                .HasMany(x => x.Categories)
                .WithOne(x => x.CategoryGroup)
                .HasForeignKey(x => x.CategoryGroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}