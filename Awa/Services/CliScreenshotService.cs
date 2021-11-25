using System.CommandLine;
using System.CommandLine.Invocation;
using Configuration;
using Core.Shared;
using Domain.Enums;
using Domain.Models;

namespace Awa;

internal class CliScreenshotService
{
    private readonly IScreenshotCreator _screenshotCreator;
    private readonly HttpService _httpService;
    private readonly Spinner _spinner;
    
    public CliScreenshotService(IScreenshotCreator screenshotCreator)
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
                "URL")
        };

        command.Description = "Areawa new archive...";

        command.Handler = CommandHandler.Create<string>(TakeScreenshotAsync);

        return command;
    }
    
    private async Task TakeScreenshotAsync(string url)
    {
        _spinner.Start();
        
        var source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;

        var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey);
        
        var file = new ArchiveFile
        {
            Filename = "subor",
            Extension = ArchiveType.Pdf,
            Folder = "docasnyfolder",
            SourceUrl = url
        };
        var stream = await _screenshotCreator.TakeScreenshotStreamAsync(file, cancellationToken);

        await _httpService.PostAsync(apiKey, stream, cancellationToken);
        
        _spinner.Stop();
        
        Console.WriteLine($"Done");
    }
}