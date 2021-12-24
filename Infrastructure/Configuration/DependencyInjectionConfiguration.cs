using Core.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IScreenshotCreator, ScreenshotCreator>();
        services.AddTransient<IStorageService, AzureBlobStorageService>();
        services.AddTransient<IQueueService, AzureStorageQueueService>();
        services.AddTransient<IHttpService, HttpService>();
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }
}