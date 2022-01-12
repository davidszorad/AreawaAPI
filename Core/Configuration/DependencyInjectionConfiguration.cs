global using Core.CategoriesManagement;
global using Core.WatchDog;
global using Core.Shared;
global using Core.UserManagement;
global using Core.WebsiteArchiveCreator;
global using Core.WebsiteArchiveReader;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
    {
        services.AddTransient<IWebsiteArchiveReaderService, WebsiteArchiveReaderService>();
        services.AddTransient<IWebsiteArchiveCreatorService, WebsiteArchiveCreatorService>();
        services.AddTransient<ICategoriesService, CategoriesService>();
        services.AddTransient<IPoisonQueueService, PoisonQueueService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IWatchDogService, WatchDogService>();
        return services;
    }
}