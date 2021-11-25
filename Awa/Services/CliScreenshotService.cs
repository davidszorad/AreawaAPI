using System.CommandLine;
using System.CommandLine.Invocation;
using Configuration;

namespace Awa;

internal class CliScreenshotService
{
    private readonly Spinner _spinner;
    
    public CliScreenshotService()
    {
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

        command.Description = "Areawa login via API key.";

        command.Handler = CommandHandler.Create<string>(TakeScreenshotAsync);

        return command;
    }
    
    private async Task TakeScreenshotAsync(string url)
    {
        _spinner.Start();

        var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey);
        
        _spinner.Stop();
        
        Console.WriteLine($"Done. API key {url} was logged in.");
    }
}