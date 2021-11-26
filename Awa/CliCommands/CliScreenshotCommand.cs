using System.CommandLine;
using System.CommandLine.Invocation;
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

        var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey, cancellationToken);
        
        var stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, cancellationToken);

        await _httpService.PostAsync(apiKey, stream, cancellationToken);
        
        _spinner.Stop();
        
        Console.WriteLine($"Done");
    }
}