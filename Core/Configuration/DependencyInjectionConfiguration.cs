using Core.Reader;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient<IReaderService, ReaderService>();
            return services;
        }
    }
}