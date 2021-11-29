using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Configuration;
using Core.Shared;
using Domain.Enums;

namespace Awa;

internal class CliScreenshotCommand
{
    private readonly IScreenshotCreator _screenshotCreator;
    private readonly HttpService _httpService;
    private readonly Spinner _spinner;
    
    public CliScreenshotCommand(IScreenshotCreator screenshotCreator)
    {
        _screenshotCreator = screenshotCreator;
        _httpService = new HttpService();
        _spinner = new Spinner();
    }
    
    public Command Register()
    {
        var command = new Command("new")
        {
            new Argument<string>(
                "url", 
                "URL"),
            new Option<string>(new [] { "--name", "--n" }, "Name of website archive")
            {
                IsRequired = true
            },
            new Option<string>(new [] { "--description", "--d" }, "Description of website archive")
            {
                IsRequired = true
            },
            new Option(new[] { "--pdf", "-pdf", "--p" }, "PDF option"),
            new Option(new[] { "--image", "--img" }, "Image option")
        };

        command.Description = "Areawa new archive...";

        command.Handler = CommandHandler.Create<string, string, string, bool, bool, IConsole>(TakeScreenshotAsync);

        return command;
    }
    
    private async Task TakeScreenshotAsync(string url, string name, string description, bool pdf, bool image, IConsole console)
    {
        if (pdf)
        {
            console.Out.WriteLine("PDF");
        }
        if (image)
        {
            console.Out.WriteLine("IMG");
        }
        
        console.Out.WriteLine($"U:{url}; N:{name}; D:{description}; PDF:{pdf.ToString()}, IMG:{image.ToString()}");
        return;



        _spinner.Start();
        
        var source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;

        var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey, cancellationToken);
        
        var stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, cancellationToken);

        await _httpService.PostAsync(apiKey, stream, cancellationToken);
        
        _spinner.Stop();
        
        Console.WriteLine($"Done");
    }
}