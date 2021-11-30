using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text.RegularExpressions;
using Configuration;
using Core.Shared;
using Core.WebsiteArchiveCreator;
using Domain.Enums;
using Infrastructure;

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
        if (!RuntimeInfoService.IsMac())
        {
            _spinner.Start();
        }
        
        var source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;

        var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey, cancellationToken);

        Stream stream;
        if (RuntimeInfoService.IsMac())
        {
            stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, _spinner.Start, cancellationToken);   
        }
        else
        {
            stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, cancellationToken);
        }

        if (!TryGetArchiveType(pdf, image, console, out var archiveType) || !archiveType.HasValue || !IsValidUrl(url))
        {
            await stream.DisposeAsync();
            _spinner.Stop();
            console.Error.WriteLine($"Error");
        }
        
        var createArchivedWebsiteCommand = new CreateArchivedWebsiteCommand
        {
            Name = name,
            ArchiveType = archiveType.Value,
            Description = description,
            SourceUrl = url
        };
            
        var result = await _httpService.PostAsync(apiKey, stream, createArchivedWebsiteCommand, cancellationToken);
            
        await stream.DisposeAsync();
        _spinner.Stop();
        console.Out.WriteLine($"Done. { result }");
        
        // TODO: stream disposing
        // TODO: httpclient factory
        // TODO: update readme
        // TODO: update command argument descriptions
    }

    private bool TryGetArchiveType(bool isPdf, bool isImage, IConsole console, out ArchiveType? archiveType)
    {
        if (!isPdf && !isImage)
        {
            archiveType = ArchiveType.Pdf;
            return true;
        }

        if (isPdf)
        {
            archiveType = ArchiveType.Pdf;
            return true;
        }

        if (isImage)
        {
            archiveType = ArchiveType.Png;
            return true;
        }
        
        console.Error.WriteLine("Choose either PDF or IMAGE.");
        archiveType = null;
        return false;
    }
    
    private bool IsValidUrl(string url)
    {
        string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
        Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return Rgx.IsMatch(url);
    }
}