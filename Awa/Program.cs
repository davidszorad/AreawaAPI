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
                x.AddTransient<IShit, Shit>();
                x.AddTransient<IScreenshotCreator, ScreenshotCreator>();
                x.AddTransient<CliService>();
                x.AddTransient<CliLoginService>();
            })
            .Build();

        var cliService = host.Services.GetRequiredService<CliService>();
        var cliLoginService = host.Services.GetRequiredService<CliLoginService>();


        
        await Task.FromResult(0);
        
        
        var rootCommand = new RootCommand
        {
            Name = "Areawa",
            Description = "CLI for Areawa App"
        };

        //return myService.DoSomething(args);
        rootCommand.Add(cliService.Rename());
        rootCommand.Add(cliService.Create());
        rootCommand.Add(cliLoginService.RegisterLogin());
        //return rootCommand.InvokeAsync(args).Result;
        return await rootCommand.InvokeAsync(args);
    }
}