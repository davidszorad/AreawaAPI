using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Database.Migrations
{
    public partial class AddWatchDog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetryPeriod",
                schema: "type",
                columns: table => new
                {
                    RetryPeriodId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetryPeriod", x => x.RetryPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "WatchDog",
                schema: "areawa",
                columns: table => new
                {
                    WatchDogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartSelector = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EndSelector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContentHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RetryPeriodId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ApiUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchDog", x => x.WatchDogId);
                    table.ForeignKey(
                        name: "FK_WatchDog_ApiUser_ApiUserId",
                        column: x => x.ApiUserId,
                        principalSchema: "identity",
                        principalTable: "ApiUser",
                        principalColumn: "ApiUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchDog_EntityStatus_EntityStatusId",
                        column: x => x.EntityStatusId,
                        principalSchema: "type",
                        principalTable: "EntityStatus",
                        principalColumn: "EntityStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchDog_RetryPeriod_RetryPeriodId",
                        column: x => x.RetryPeriodId,
                        principalSchema: "type",
                        principalTable: "RetryPeriod",
                        principalColumn: "RetryPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchDog_ApiUserId",
                schema: "areawa",
                table: "WatchDog",
                column: "ApiUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchDog_EntityStatusId",
                schema: "areawa",
                table: "WatchDog",
                column: "EntityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchDog_PublicId",
                schema: "areawa",
                table: "WatchDog",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchDog_RetryPeriodId",
                schema: "areawa",
                table: "WatchDog",
                column: "RetryPeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchDog",
                schema: "areawa");

            migrationBuilder.DropTable(
                name: "RetryPeriod",
                schema: "type");
        }
    }
}
