using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Database.Migrations
{
    public partial class SeedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (4, 'SourceChanged')");
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (5, 'SourceNotFound')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [type].[EntityStatus] WHERE EntityStatusId IN (4, 5)");
        }
    }
}
