using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Database.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryGroup",
                schema: "areawa",
                columns: table => new
                {
                    CategoryGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroup", x => x.CategoryGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "areawa",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_CategoryGroup_CategoryGroupId",
                        column: x => x.CategoryGroupId,
                        principalSchema: "areawa",
                        principalTable: "CategoryGroup",
                        principalColumn: "CategoryGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteArchiveCategory",
                schema: "areawa",
                columns: table => new
                {
                    WebsiteArchiveId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteArchiveCategory", x => new { x.WebsiteArchiveId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_WebsiteArchiveCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "areawa",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebsiteArchiveCategory_WebsiteArchive_WebsiteArchiveId",
                        column: x => x.WebsiteArchiveId,
                        principalSchema: "areawa",
                        principalTable: "WebsiteArchive",
                        principalColumn: "WebsiteArchiveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryGroupId",
                schema: "areawa",
                table: "Category",
                column: "CategoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_PublicId",
                schema: "areawa",
                table: "Category",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroup_PublicId",
                schema: "areawa",
                table: "CategoryGroup",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchiveCategory_CategoryId",
                schema: "areawa",
                table: "WebsiteArchiveCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteArchiveCategory",
                schema: "areawa");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "areawa");

            migrationBuilder.DropTable(
                name: "CategoryGroup",
                schema: "areawa");
        }
    }
}
