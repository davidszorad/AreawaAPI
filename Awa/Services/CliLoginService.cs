using System.CommandLine;
using System.CommandLine.Invocation;
using Configuration;

namespace Awa;

internal class CliLoginService
{
    private readonly Spinner _spinner;
    
    public CliLoginService()
    {
        _spinner = new Spinner();
    }
    
    public Command RegisterLogin()
    {
        var command = new Command("login")
        {
            new Argument<string>(
                "key", 
                "API Key")
        };

        command.Description = "Areawa login via API key.";

        command.Handler = CommandHandler.Create<string>(LoginAsync);

        return command;
    }
    
    private async Task LoginAsync(string key)
    {
        _spinner.Start();

        await FileSystemService.SaveTextAsync(key, ConfigurationConstants.FileNameWithApiKey);
        
        _spinner.Stop();
        
        Console.WriteLine($"Done. API key {key} was logged in.");
    }
}