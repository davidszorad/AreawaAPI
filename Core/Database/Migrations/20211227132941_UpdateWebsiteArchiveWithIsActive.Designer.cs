// <auto-generated />
using System;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Core.Database.Migrations
{
    [DbContext(typeof(AreawaDbContext))]
    [Migration("20211227132941_UpdateWebsiteArchiveWithIsActive")]
    partial class UpdateWebsiteArchiveWithIsActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("areawa")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Database.Entities.ApiUser", b =>
                {
                    b.Property<long>("ApiUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ApiUserId"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPremium")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ApiUserId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.ToTable("ApiUser", "identity");
                });

            modelBuilder.Entity("Core.Database.Entities.ArchiveType", b =>
                {
                    b.Property<int>("ArchiveTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ArchiveTypeId");

                    b.ToTable("ArchiveType", "type");
                });

            modelBuilder.Entity("Core.Database.Entities.Category", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryId"), 1L, 1);

                    b.Property<long>("ApiUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CategoryGroupId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.HasIndex("ApiUserId");

                    b.HasIndex("CategoryGroupId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.ToTable("Category", "areawa");
                });

            modelBuilder.Entity("Core.Database.Entities.CategoryGroup", b =>
                {
                    b.Property<long>("CategoryGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryGroupId"), 1L, 1);

                    b.Property<long>("ApiUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryGroupId");

                    b.HasIndex("ApiUserId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.ToTable("CategoryGroup", "areawa");
                });

            modelBuilder.Entity("Core.Database.Entities.EntityStatus", b =>
                {
                    b.Property<int>("EntityStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("EntityStatusId");

                    b.ToTable("EntityStatus", "type");
                });

            modelBuilder.Entity("Core.Database.Entities.RetryPeriod", b =>
                {
                    b.Property<int>("RetryPeriodId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("RetryPeriodId");

                    b.ToTable("RetryPeriod", "type");
                });

            modelBuilder.Entity("Core.Database.Entities.WatchDog", b =>
                {
                    b.Property<long>("WatchDogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("WatchDogId"), 1L, 1);

                    b.Property<long>("ApiUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("ContentHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndSelector")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("EntityStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RetryPeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<long>("ScanCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<string>("StartSelector")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WatchDogId");

                    b.HasIndex("ApiUserId");

                    b.HasIndex("EntityStatusId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.HasIndex("RetryPeriodId");

                    b.ToTable("WatchDog", "areawa");
                });

            modelBuilder.Entity("Core.Database.Entities.WebsiteArchive", b =>
                {
                    b.Property<long>("WebsiteArchiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("WebsiteArchiveId"), 1L, 1);

                    b.Property<long>("ApiUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("ArchiveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("ArchiveUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("WebsiteArchiveId");

                    b.HasIndex("ApiUserId");

                    b.HasIndex("ArchiveTypeId");

                    b.HasIndex("EntityStatusId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.HasIndex("ShortId")
                        .IsUnique();

                    b.ToTable("WebsiteArchive", "areawa");
                });

            modelBuilder.Entity("Core.Database.Entities.WebsiteArchiveCategory", b =>
                {
                    b.Property<long>("WebsiteArchiveId")
                        .HasColumnType("bigint");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.HasKey("WebsiteArchiveId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("WebsiteArchiveCategory", "areawa");
                });

            modelBuilder.Entity("Core.Database.Entities.Category", b =>
                {
                    b.HasOne("Core.Database.Entities.ApiUser", "ApiUser")
                        .WithMany("Categories")
                        .HasForeignKey("ApiUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ApiUser");

                    b.Navigation("CategoryGroup");
                });

            modelBuilder.Entity("Core.Database.Entities.CategoryGroup", b =>
                {
                    b.HasOne("Core.Database.Entities.ApiUser", "ApiUser")
                        .WithMany("CategoryGroups")
                        .HasForeignKey("ApiUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApiUser");
                });

            modelBuilder.Entity("Core.Database.Entities.WatchDog", b =>
                {
                    b.HasOne("Core.Database.Entities.ApiUser", "ApiUser")
                        .WithMany()
                        .HasForeignKey("ApiUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.EntityStatus", null)
                        .WithMany("ChangeTrackers")
                        .HasForeignKey("EntityStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.RetryPeriod", null)
                        .WithMany("WatchDogs")
                        .HasForeignKey("RetryPeriodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApiUser");
                });

            modelBuilder.Entity("Core.Database.Entities.WebsiteArchive", b =>
                {
                    b.HasOne("Core.Database.Entities.ApiUser", "ApiUser")
                        .WithMany("WebsiteArchives")
                        .HasForeignKey("ApiUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.ArchiveType", null)
                        .WithMany("WebsiteArchives")
                        .HasForeignKey("ArchiveTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.EntityStatus", null)
                        .WithMany("WebsiteArchives")
                        .HasForeignKey("EntityStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApiUser");
                });

            modelBuilder.Entity("Core.Database.Entities.WebsiteArchiveCategory", b =>
                {
                    b.HasOne("Core.Database.Entities.Category", "Category")
                        .WithMany("WebsiteArchiveCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Database.Entities.WebsiteArchive", "WebsiteArchive")
                        .WithMany("WebsiteArchiveCategories")
                        .HasForeignKey("WebsiteArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("WebsiteArchive");
                });

            modelBuilder.Entity("Core.Database.Entities.ApiUser", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("CategoryGroups");

                    b.Navigation("WebsiteArchives");
                });

            modelBuilder.Entity("Core.Database.Entities.ArchiveType", b =>
                {
                    b.Navigation("WebsiteArchives");
                });

            modelBuilder.Entity("Core.Database.Entities.Category", b =>
                {
                    b.Navigation("WebsiteArchiveCategories");
                });

            modelBuilder.Entity("Core.Database.Entities.CategoryGroup", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Core.Database.Entities.EntityStatus", b =>
                {
                    b.Navigation("ChangeTrackers");

                    b.Navigation("WebsiteArchives");
                });

            modelBuilder.Entity("Core.Database.Entities.RetryPeriod", b =>
                {
                    b.Navigation("WatchDogs");
                });

            modelBuilder.Entity("Core.Database.Entities.WebsiteArchive", b =>
                {
                    b.Navigation("WebsiteArchiveCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
