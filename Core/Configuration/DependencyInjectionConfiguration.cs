using Core.Processor;
using Core.Reader;
using Core.Scheduler;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IReaderService, ReaderService>();
            services.AddTransient<ISchedulerService, SchedulerService>();
            services.AddTransient<IProcessorService, ProcessorService>();
            return services;
        }
    }
}