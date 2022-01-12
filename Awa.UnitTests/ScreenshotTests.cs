using System.Threading.Tasks;
using Core.Shared;
using Infrastructure;
using NUnit.Framework;

namespace Awa.UnitTests;

[TestFixture]
public class ScreenshotTests
{
    private ScreenshotService _screenshotService;
    
    [SetUp]
    public void SetUp()
    {
        IScreenshotCreator screenshotCreator = new ScreenshotCreator();
        _screenshotService = new ScreenshotService(screenshotCreator);
    }
    
    [Test]
    public async Task TakeScreenshotAsync_CreatesAndUploadsScreenshot_ScreenshotIsCreatedAndUploaded()
    {
        var response = await _screenshotService.TakeScreenshotAsync(
            "https://www.google.com",
            "Google",
            "Google Home Page",
            true,
            false,
            () => { });

        Assert.That(response, Does.Contain("Done"));
    }
}