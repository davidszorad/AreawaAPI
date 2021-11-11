using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;

namespace Infrastructure;

public class ScreenshotCreator : IScreenshotCreator
{
    private readonly FileSystemService _fileSystemService;
    public ScreenshotCreator(IWebHostEnvironment host)
    {
        _fileSystemService = new FileSystemService(host.WebRootPath);
    }
    
    public async Task<string> CreateAsync(ArchiveFile file, CancellationToken cancellationToken = default)
    {
        var outputFile = _fileSystemService.PrepareFile(file);
            
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

        return "TODO";
    }
}