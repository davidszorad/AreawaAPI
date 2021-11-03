using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Database.Migrations
{
    public partial class SeedArchiveTypeAndEntityStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [type].[ArchiveType] (ArchiveTypeId, Name) VALUES (1, 'Pdf')");
            migrationBuilder.Sql("INSERT INTO [type].[ArchiveType] (ArchiveTypeId, Name) VALUES (2, 'Png')");
            
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (0, 'Error')");
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (1, 'Pending')");
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (2, 'Processing')");
            migrationBuilder.Sql("INSERT INTO [type].[EntityStatus] (EntityStatusId, Name) VALUES (3, 'Ok')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [type].[ArchiveType] WHERE ArchiveTypeId IN (1, 2)");
            migrationBuilder.Sql("DELETE FROM [type].[EntityStatus] WHERE EntityStatusId IN (0, 1, 2, 3)");
        }
    }
}
