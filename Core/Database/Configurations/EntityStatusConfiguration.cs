using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Configurations
{
    internal class EntityStatusConfiguration : ConfigurationBase, IEntityTypeConfiguration<EntityStatus>
    {
        public void Configure(EntityTypeBuilder<EntityStatus> builder)
        {
            builder.ToTable(nameof(EntityStatus), SchemaType);

            builder.HasKey(x => x.EntityStatusId);
            builder.Property( x => x.EntityStatusId).ValueGeneratedNever();

            builder.Property(a => a.Name).HasMaxLength(255).IsRequired();
            
            builder
                .HasMany(x => x.WebsiteArchives)
                .WithOne()
                .HasForeignKey(x => x.EntityStatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}