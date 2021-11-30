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
                x.AddTransient<CliLoginCommand>();
                x.AddTransient<CliScreenshotCommand>();
            })
            .Build();

        var cliLoginCommand = host.Services.GetRequiredService<CliLoginCommand>();
        var cliScreenshotCommand = host.Services.GetRequiredService<CliScreenshotCommand>();

        var rootCommand = new RootCommand
        {
            Name = "Areawa",
            Description = "CLI for Areawa app"
        };

        rootCommand.Add(cliLoginCommand.Register());
        rootCommand.Add(cliScreenshotCommand.Register());
        return await rootCommand.InvokeAsync(args);
    }
}