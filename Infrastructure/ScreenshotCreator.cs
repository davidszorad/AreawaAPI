using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using PuppeteerSharp;

namespace Infrastructure
{
    public class ScreenshotCreator : IScreenshotCreator
    {
        public async Task<string> CreateAsync(string sourceUrl, CancellationToken cancellationToken = default)
        {
            string folder = "output";
            if (!Directory.Exists(Path.GetFullPath(folder)))
            {
                Directory.CreateDirectory(Path.GetFullPath(folder));
            }

            var outputFile = Path.Combine(folder, Path.GetFileName("Usage.png"));
            var outputFilePdf = Path.Combine(folder,Path.GetFileName("Usage.pdf"));
            var fileInfo = new FileInfo(outputFile);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            // await page.SetViewportAsync(new ViewPortOptions
            // {
            //     Width = 1920,
            //     Height = 50000
            // });
            await page.GoToAsync(sourceUrl);
            await Task.Delay(5000, cancellationToken);

            await page.ScreenshotAsync(outputFile);
            await page.PdfAsync(outputFilePdf);

            return "TODO";
        }
    }
}