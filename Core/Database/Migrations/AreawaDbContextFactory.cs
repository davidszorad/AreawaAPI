using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Database.Migrations;

internal class AreawaDbContextFactory : IDesignTimeDbContextFactory<AreawaDbContext>
{
    public AreawaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AreawaDbContext>();
        optionsBuilder.UseSqlServer(ConfigStore.GetDbConnectionString());

        return new AreawaDbContext(optionsBuilder.Options);
    }
}