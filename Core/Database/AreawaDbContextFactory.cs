using Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Database
{
    public class AreawaDbContextFactory : IDesignTimeDbContextFactory<AreawaDbContext>
    {
        public AreawaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AreawaDbContext>();
            optionsBuilder.UseSqlServer(ConfigStore.GetValue("dbconnectionstring"));

            return new AreawaDbContext(optionsBuilder.Options);
        }
    }
}