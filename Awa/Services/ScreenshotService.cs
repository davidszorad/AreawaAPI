using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Configuration;
using Core.Shared;
using Core.WebsiteArchiveCreator;
using Domain.Enums;
using Infrastructure;

[assembly: InternalsVisibleTo("Awa.UnitTests")]

namespace Awa;

internal class ScreenshotService
{
    private readonly IScreenshotCreator _screenshotCreator;
    private readonly HttpService _httpService;
    
    public ScreenshotService(IScreenshotCreator screenshotCreator)
    {
        _screenshotCreator = screenshotCreator;
        _httpService = new HttpService();
    }
    
    public async Task<string> TakeScreenshotAsync(string url, string name, string description, bool pdf, bool image, Action runBeforeScreenshot, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiKey = await FileSystemService.ReadTextAsync(ConfigurationConstants.FileNameWithApiKey, cancellationToken);

            Stream stream;
            if (RuntimeInfoService.IsMac())
            {
                stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, runBeforeScreenshot, cancellationToken);   
            }
            else
            {
                stream = await _screenshotCreator.TakeScreenshotStreamAsync(url, ArchiveType.Pdf, cancellationToken);
            }

            if (!TryGetArchiveType(pdf, image, out var archiveType) || !archiveType.HasValue || !IsValidUrl(url))
            {
                await stream.DisposeAsync();
                return "Error: Choose either PDF or IMAGE.";
            }
        
            var createArchivedWebsiteCommand = new CreateArchivedWebsiteCommand
            {
                Name = name,
                ArchiveType = archiveType ?? ArchiveType.Pdf,
                Description = description,
                SourceUrl = url
            };
            
            var result = await _httpService.PostAsync(apiKey, stream, createArchivedWebsiteCommand, cancellationToken);
            
            await stream.DisposeAsync();
            return $"Done. { result }";
        }
        catch (Exception)
        {
            return "Unspecified error happened. Try again.";
        }
    }
    
    private bool TryGetArchiveType(bool isPdf, bool isImage, out ArchiveType? archiveType)
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