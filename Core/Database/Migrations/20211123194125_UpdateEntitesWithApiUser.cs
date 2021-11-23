using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Database.Migrations
{
    public partial class UpdateEntitesWithApiUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteArchive_ApiUser_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive");

            migrationBuilder.AddColumn<long>(
                name: "ApiUserId",
                schema: "areawa",
                table: "CategoryGroup",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ApiUserId",
                schema: "areawa",
                table: "Category",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroup_ApiUserId",
                schema: "areawa",
                table: "CategoryGroup",
                column: "ApiUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ApiUserId",
                schema: "areawa",
                table: "Category",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ApiUser_ApiUserId",
                schema: "areawa",
                table: "Category",
                column: "ApiUserId",
                principalSchema: "identity",
                principalTable: "ApiUser",
                principalColumn: "ApiUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGroup_ApiUser_ApiUserId",
                schema: "areawa",
                table: "CategoryGroup",
                column: "ApiUserId",
                principalSchema: "identity",
                principalTable: "ApiUser",
                principalColumn: "ApiUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteArchive_ApiUser_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive",
                column: "ApiUserId",
                principalSchema: "identity",
                principalTable: "ApiUser",
                principalColumn: "ApiUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ApiUser_ApiUserId",
                schema: "areawa",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGroup_ApiUser_ApiUserId",
                schema: "areawa",
                table: "CategoryGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteArchive_ApiUser_ApiUserId",
                schema: "areawa",
                table: "WebsiteArchive");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGroup_ApiUserId",
                schema: "areawa",
                table: "CategoryGroup");

            migrationBuilder.DropIndex(
                name: "IX_Category_ApiUserId",
                schema: "areawa",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ApiUserId",
                schema: "areawa",
                table: "CategoryGroup");

            migrationBuilder.DropColumn(
                name: "ApiUserId",
                schema: "areawa",
                table: "Category");

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
    }
}
