﻿using System.CommandLine;
using System.CommandLine.Invocation;
using Core.Shared;
using Domain.Enums;
using Domain.Models;

namespace Awa;

internal class CliCommand
{
    private readonly IScreenshotCreator _screenshotCreator;

    public CliCommand(IScreenshotCreator screenshotCreator)
    {
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