using Configuration;
using Core.Configuration;
using Core.Database;
using Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Areawa.Workers;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(x =>
            {
                x.RegisterCoreDependencies();
                x.RegisterInfrastructureDependencies();
                x.AddDbContext<AreawaDbContext>(options => options.UseSqlServer(ConfigStore.GetDbConnectionString()));
            })
            .Build();
            
        host.Run();
    }
}