using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Shared;
using Domain.Enums;
using Domain.Models;
using PuppeteerSharp;

namespace Infrastructure;

public class ScreenshotCreator : IScreenshotCreator
{
    public async Task<Stream> TakeScreenshotStreamAsync(ArchiveFile file, CancellationToken cancellationToken = default)
    {
        var browserFetcher = new BrowserFetcher();
        if (!IsMac())
        {
            var browserFetcherOptions = new BrowserFetcherOptions
            {
                Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationConstants.ProfileFolder, ".local-chromium")
            };
            browserFetcher = new BrowserFetcher(browserFetcherOptions);
        }
        await browserFetcher.DownloadAsync();
        var revisionInfo = await browserFetcher.GetRevisionInfoAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, ExecutablePath = browserFetcher.GetExecutablePath(revisionInfo.Revision) });
        
        await using var page = await browser.NewPageAsync();
        // await page.SetViewportAsync(new ViewPortOptions
        // {
        //     Width = 1920,
        //     Height = 50000
        // });
        await page.GoToAsync(file.SourceUrl);
        await Task.Delay(5000, cancellationToken);

        switch (file.Extension)
        {
            case ArchiveType.Pdf:
                var pdfStream = await page.PdfStreamAsync();
                if (IsMac() && Directory.Exists(".local-chromium"))
                {
                    Directory.Delete(".local-chromium", true);
                }
                return pdfStream;
            case ArchiveType.Png:
                var imageStream = await page.ScreenshotStreamAsync();
                if (IsMac() && Directory.Exists(".local-chromium"))
                {
                    Directory.Delete(".local-chromium", true);
                }
                return imageStream;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static bool IsMac()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}