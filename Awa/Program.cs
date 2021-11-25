using System.CommandLine;
using Awa;
using Core.Shared;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class App
{
    public static async Task<int> Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureServices(x =>
            {
                x.AddTransient<IScreenshotCreator, ScreenshotCreator>();
                x.AddTransient<CliService>();
                x.AddTransient<CliLoginService>();
                x.AddTransient<CliScreenshotService>();
            })
            .Build();

        var cliService = host.Services.GetRequiredService<CliService>();
        var cliLoginService = host.Services.GetRequiredService<CliLoginService>();
        var cliScreenshotService = host.Services.GetRequiredService<CliScreenshotService>();

        var rootCommand = new RootCommand
        {
            Name = "Areawa",
            Description = "CLI for Areawa App"
        };

        rootCommand.Add(cliService.Rename());
        rootCommand.Add(cliService.Create());
        rootCommand.Add(cliLoginService.Register());
        rootCommand.Add(cliScreenshotService.Register());
        return await rootCommand.InvokeAsync(args);
    }
}