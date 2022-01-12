using System.Threading.Tasks;
using Configuration;
using Core.Database;
using Core.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Core.UnitTests;

public class EntityFrameworkTests
{
    [Test]
    public async Task ExecuteCustomQueryTest()
    {
        var loggerFactory = LoggerFactory.Create(log =>
        {
            log.AddProvider(new SimpleLoggerProvider());
            log.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
        });

        var builder = new DbContextOptionsBuilder<AreawaDbContext>()
            .UseSqlServer(ConfigStore.GetDbConnectionString())
            .UseLoggerFactory(loggerFactory);
        var dbContext = new AreawaDbContext(builder.Options);
        
        // run any query on dbContext that will be executed on sql server db
        var user = await dbContext.ApiUser.FirstOrDefaultAsync();
        
        Assert.That(user, Is.Not.Null);
    }
}