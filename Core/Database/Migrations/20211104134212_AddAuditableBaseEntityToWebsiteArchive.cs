using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Database.Migrations
{
    public partial class AddAuditableBaseEntityToWebsiteArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "areawa",
                table: "WebsiteArchive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "areawa",
                table: "WebsiteArchive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "areawa",
                table: "WebsiteArchive");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "areawa",
                table: "WebsiteArchive");
        }
    }
}
