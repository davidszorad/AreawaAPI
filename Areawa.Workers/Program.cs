using Configuration;
using Core.Configuration;
using Core.Database;
using Core.Shared;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Areawa.Workers
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(x =>
                {
                    x.RegisterCoreDependencies();
                    x.AddTransient<IQueueService, AzureStorageQueueService>();
                    x.AddTransient<IScreenshotCreator, ScreenshotCreator>();
                    x.AddTransient<IStorageService, FileSystemStorageService>();
                    x.AddDbContext<AreawaDbContext>(options => options.UseSqlServer(ConfigStore.GetDbConnectionString()));
                })
                .Build();
            
            host.Run();
        }
    }
}