using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Database.Migrations
{
    public partial class SeedRetryPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [type].[RetryPeriod] (RetryPeriodId, Name) VALUES (1, 'OneWeek')");
            migrationBuilder.Sql("INSERT INTO [type].[RetryPeriod] (RetryPeriodId, Name) VALUES (2, 'OneMonth')");
            migrationBuilder.Sql("INSERT INTO [type].[RetryPeriod] (RetryPeriodId, Name) VALUES (3, 'TreeMonths')");
            migrationBuilder.Sql("INSERT INTO [type].[RetryPeriod] (RetryPeriodId, Name) VALUES (4, 'OneYear')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [type].[RetryPeriod] WHERE RetryPeriodId IN (1, 2, 3, 4)");
        }
    }
}
