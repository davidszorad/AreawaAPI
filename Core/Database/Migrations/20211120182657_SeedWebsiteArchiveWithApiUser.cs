using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Database.Migrations
{
    public partial class SeedWebsiteArchiveWithApiUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteArchive_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteArchive_ApiUser_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "ApiUserId",
                principalSchema: "identity",
                principalTable: "ApiUser",
                principalColumn: "ApiUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteArchive_ApiUser_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteArchive_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive");

            migrationBuilder.DropColumn(
                name: "ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive");
        }
    }
}
