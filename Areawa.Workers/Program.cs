using Configuration;
using Core.Configuration;
using Core.Database;
using Core.Shared;
using Core.WatchDog;
using Infrastructure;
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
                x.AddTransient<IQueueService, AzureStorageQueueService>();
                x.AddTransient<IStorageService, AzureBlobStorageService>();
                x.AddTransient<IWatchDogService, WatchDogService>();
                x.AddTransient<IHttpService, HttpService>();
                x.AddDbContext<AreawaDbContext>(options => options.UseSqlServer(ConfigStore.GetDbConnectionString()));
            })
            .Build();
            
        host.Run();
    }
}