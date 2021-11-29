using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Shared;
using Domain.Enums;
using PuppeteerSharp;

namespace Infrastructure;

public class ScreenshotCreator : IScreenshotCreator
{
    public async Task<Stream> TakeScreenshotStreamAsync(string sourceUrl, ArchiveType archiveType, CancellationToken cancellationToken = default)
    {
        return await TakeScreenshotStreamAsync(sourceUrl, archiveType, () => { }, cancellationToken);
    }

    public async Task<Stream> TakeScreenshotStreamAsync(string sourceUrl, ArchiveType archiveType, Action runBeforeScreenshot, CancellationToken cancellationToken = default)
    {
        var browserFetcher = new BrowserFetcher();
        if (!RuntimeInfoService.IsMac())
        {
            var browserFetcherOptions = new BrowserFetcherOptions
            {
                Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationConstants.ProfileFolder, ".local-chromium")
            };
            browserFetcher = new BrowserFetcher(browserFetcherOptions);
        }
        await browserFetcher.DownloadAsync();
        runBeforeScreenshot?.Invoke();
        var revisionInfo = await browserFetcher.GetRevisionInfoAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, ExecutablePath = browserFetcher.GetExecutablePath(revisionInfo.Revision) });
        
        await using var page = await browser.NewPageAsync();
        // TODO: await page.SetViewportAsync(new ViewPortOptions
        // {
        //     Width = 1920,
        //     Height = 50000
        // });
        await page.GoToAsync(sourceUrl);
        await Task.Delay(5000, cancellationToken);

        switch (archiveType)
        {
            case ArchiveType.Pdf:
                var pdfStream = await page.PdfStreamAsync();
                CleanChromiumFolder();
                return pdfStream;
            case ArchiveType.Png:
                var imageStream = await page.ScreenshotStreamAsync();
                CleanChromiumFolder();
                return imageStream;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CleanChromiumFolder()
    {
        if (RuntimeInfoService.IsMac() && Directory.Exists(".local-chromium"))
        {
            Directory.Delete(".local-chromium", true);
        }
    }
}