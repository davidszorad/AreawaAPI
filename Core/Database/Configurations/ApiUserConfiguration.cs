using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations;

internal class ApiUserConfiguration : ConfigurationBase, IEntityTypeConfiguration<ApiUser>
{
    public void Configure(EntityTypeBuilder<ApiUser> builder)
    {
        builder.ToTable(nameof(ApiUser), SchemaIdentity);
        
        builder.HasKey(x => x.ApiUserId);
        builder.HasIndex(x => x.PublicId).IsUnique();
        
        builder.Property(x => x.PublicId).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.IsPremium).HasDefaultValue(false).IsRequired();

        builder
            .HasMany(x => x.WebsiteArchives)
            .WithOne(x => x.ApiUser)
            .HasForeignKey(x => x.ApiUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}