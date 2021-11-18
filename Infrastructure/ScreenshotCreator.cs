using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;

namespace Infrastructure;

public class ScreenshotCreator : IScreenshotCreator
{
    private readonly LocalFileService _localFileService;
    public ScreenshotCreator(/*IWebHostEnvironment host*/)
    {
        //_localFileService = new LocalFileService(host.WebRootPath);
        _localFileService = new LocalFileService("");
    }
    
    public async Task<string> TakeScreenshotAsync(ArchiveFile file, CancellationToken cancellationToken = default)
    {
        var outputFile = _localFileService.PrepareEmptyFile(file);
            
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
                await page.PdfAsync(outputFile);
                break;
            case ArchiveType.Png:
                await page.ScreenshotAsync(outputFile);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return outputFile;
    }
    
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
                await page.PdfStreamAsync();
                break;
            case ArchiveType.Png:
                await page.ScreenshotStreamAsync();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw new InvalidOperationException();
    }

    public void Cleanup(string screenshotPath)
    {
        _localFileService.CleanUp(screenshotPath);
    }
}