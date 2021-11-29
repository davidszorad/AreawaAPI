using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Configuration;

namespace Awa;

internal class CliLoginCommand
{
    private readonly Spinner _spinner;
    
    public CliLoginCommand()
    {
        _spinner = new Spinner();
    }
    
    public Command Register()
    {
        var command = new Command("login")
        {
            new Argument<string>(
                "key", 
                "API Key")
        };

        command.Description = "Areawa login via API key.";

        command.Handler = CommandHandler.Create<string, IConsole>(LoginAsync);

        return command;
    }
    
    private async Task LoginAsync(string key, IConsole console)
    {
        _spinner.Start();

        await FileSystemService.SaveTextAsync(key, ConfigurationConstants.FileNameWithApiKey);
        
        _spinner.Stop();
        
        console.Out.WriteLine($"Done. API key {key} was logged in.");
    }
}