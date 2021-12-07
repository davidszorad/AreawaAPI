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
        var context = new AreawaDbContext(builder.Options);
        
        // add any query on top of context that will be executed on SQL server DB
        
        await Task.FromResult(0);
    }
}