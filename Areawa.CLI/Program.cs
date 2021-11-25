using System.CommandLine;
using System.CommandLine.Invocation;
using Areawa.CLI;
using Core.Shared;
using Domain.Enums;
using Domain.Models;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class App
{
    private static async Task<int> Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureServices(x =>
            {
                x.AddTransient<IShit, Shit>();
                x.AddTransient<IScreenshotCreator, ScreenshotCreator>();
                x.AddTransient<MyService>();
            })
            .Build();

        var myService = host.Services.GetRequiredService<MyService>();


        
        await Task.FromResult(0);
        
        
        var rootCommand = new RootCommand
        {
            Name = "FART",
            Description = "File Association and Rename Tool"
        };

        //return myService.DoSomething(args);
        rootCommand.Add(myService.Rename());
        rootCommand.Add(myService.Create());
        //return rootCommand.InvokeAsync(args).Result;
        return await rootCommand.InvokeAsync(args);
        
        //return await Task.FromResult(0);
        
        /*
        PS C:\p_dev\Areawa\Areawa.CLI> dotnet run create 34 --unit C
        Result: 93,2 °Fee
        PS C:\p_dev\Areawa\Areawa.CLI> dotnet run rename --int-option 123
        The value for --int-option is: 123
        The value for --bool-option is: False
        The value for --file-option is: null
         */
    }
}

internal class MyService
{
    private readonly IShit _shit;
    private readonly IScreenshotCreator _screenshotCreator;

    public MyService(IShit shit, IScreenshotCreator screenshotCreator)
    {
        _shit = shit;
        _screenshotCreator = screenshotCreator;
    }

    public Command Rename()
    {
        // Create a root command with some options
        var renameCommand = new Command("rename")
        {
            new Option<int>(
                "--int-option",
                getDefaultValue: () => 42,
                description: "An option whose argument is parsed as an int"),
            new Option<bool>(
                "--bool-option",
                "An option whose argument is parsed as a bool"),
            new Option<FileInfo>(
                "--file-option",
                "An option whose argument is parsed as a FileInfo")
        };

        renameCommand.Description = "My sample app";

        // Note that the parameters of the handler method are matched according to the names of the options
        renameCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
        {
            Console.WriteLine($"The value for --int-option is: {intOption}");
            Console.WriteLine($"The value for --bool-option is: {boolOption}");
            Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
        });

        // Parse the incoming args and invoke the handler
        return renameCommand;
        
    }
    
    public Command Create()
    {
        var unit = new Option<string>
            ("--unit", "Unit of measurement");
            unit.IsRequired = true;
            unit.AddAlias("--u");
        
        // Create a root command with some options
        var createCommand = new Command("create")
        {
            new Argument<double>(
                "temperature", 
                "Temperature value"),
            unit
        };

        createCommand.Description = "My sample app";

        // Note that the parameters of the handler method are matched according to the names of the options
        createCommand.Handler = CommandHandler.Create
            <double, string>(ShowOutputAsync);

        // Parse the incoming args and invoke the handler
        return createCommand;
        
    }
    
    private async Task ShowOutputAsync(double temperature, string unit)
    {
        var file = new ArchiveFile
        {
            Filename = "subor",
            Extension = ArchiveType.Pdf,
            Folder = "docasnyfolder",
            SourceUrl = "https://dev-trips.com/dev/how-to-create-classes-that-protect-its-data"
        };

        var spinner = new Spinner();
        spinner.Start();

        await _screenshotCreator.TakeScreenshotStreamAsync(file);
        
        
        // C to F
        if(unit =="C")
        {
            temperature = (temperature * 9) / 5 + 32;
            unit = "Fee";
        }

        // F to C
        if(unit =="F")
        {
            temperature = (temperature - 32) *5 / 9;
            unit = "Cee";
        }
        spinner.Stop();
        Console.WriteLine($"Result: {temperature} \x00B0{unit}");
    }
}