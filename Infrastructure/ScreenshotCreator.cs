using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Domain.Enums;
using PuppeteerSharp;

namespace Infrastructure;

public class ScreenshotCreator : IScreenshotCreator
{
    public async Task<Stream> TakeScreenshotStreamAsync(ArchiveFile file, CancellationToken cancellationToken = default)
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
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
                return await page.PdfStreamAsync();
            case ArchiveType.Png:
                return await page.ScreenshotStreamAsync();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}