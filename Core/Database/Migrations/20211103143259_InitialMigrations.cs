using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Database.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "type");

            migrationBuilder.EnsureSchema(
                name: "areawa");

            migrationBuilder.CreateTable(
                name: "ArchiveType",
                schema: "type",
                columns: table => new
                {
                    ArchiveTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveType", x => x.ArchiveTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EntityStatus",
                schema: "type",
                columns: table => new
                {
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStatus", x => x.EntityStatusId);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteArchive",
                schema: "areawa",
                columns: table => new
                {
                    WebsiteArchiveId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchiveUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchiveTypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteArchive", x => x.WebsiteArchiveId);
                    table.ForeignKey(
                        name: "FK_WebsiteArchive_ArchiveType_ArchiveTypeId",
                        column: x => x.ArchiveTypeId,
                        principalSchema: "type",
                        principalTable: "ArchiveType",
                        principalColumn: "ArchiveTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebsiteArchive_EntityStatus_EntityStatusId",
                        column: x => x.EntityStatusId,
                        principalSchema: "type",
                        principalTable: "EntityStatus",
                        principalColumn: "EntityStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchive_ArchiveTypeId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "ArchiveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchive_EntityStatusId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "EntityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchive_PublicId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchive_ShortId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "ShortId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteArchive",
                schema: "areawa");

            migrationBuilder.DropTable(
                name: "ArchiveType",
                schema: "type");

            migrationBuilder.DropTable(
                name: "EntityStatus",
                schema: "type");
        }
    }
}
