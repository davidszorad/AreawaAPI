using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Core.Shared;
using Infrastructure;

namespace Awa;

internal class CliScreenshotCommand
{
    private readonly ScreenshotService _screenshotService;
    private readonly Spinner _spinner;
    
    public CliScreenshotCommand(IScreenshotCreator screenshotCreator)
    {
        _screenshotService = new ScreenshotService(screenshotCreator);
        _spinner = new Spinner();
    }
    
    public Command Register()
    {
        var command = new Command("new")
        {
            new Argument<string>(
                "url", 
                "URL that will be exported as a screenshot"),
            new Option<string>(new [] { "--name", "--n" }, "Name of website archive")
            {
                IsRequired = true
            },
            new Option<string>(new [] { "--description", "--d" }, "Description of website archive")
            {
                IsRequired = true
            },
            new Option(new[] { "--pdf", "--p" }, "PDF output option"),
            new Option(new[] { "--image", "--img", "--i" }, "Image output option")
        };

        command.Description = "Generate and save a screenshot of a website.";

        command.Handler = CommandHandler.Create<string, string, string, bool, bool, IConsole>(TakeScreenshotAsync);

        return command;
    }

    private async Task TakeScreenshotAsync(string url, string name, string description, bool pdf, bool image, IConsole console)
    {
        if (!RuntimeInfoService.IsMac())
        {
            _spinner.Start();
        }
        
        var source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;

        var response = await _screenshotService.TakeScreenshotAsync(url, name, description, pdf, image, _spinner.Start, cancellationToken);
        
        _spinner.Stop();
        console.Error.WriteLine(response);
    }
}