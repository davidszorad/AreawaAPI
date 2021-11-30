using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;

namespace Infrastructure.UnitTests
{
    [TestFixture]
    public class ScreenshotCreatorTests
    {
        [Test]
        public async Task TakeScreenshotAsync_ScreenshotIsGenerated_GeneratesPdfOutput()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.WebRootPath)
                .Returns("");
            
            // var archiveFile = new ArchiveFile
            // {
            //     SourceUrl = "https://dev-trips.com/dev/core-unit-testing-techniques",
            //     Filename = "unit-testing",
            //     Folder = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow:MM}-{DateTime.UtcNow:dd}-{DateTime.UtcNow:HH}-{DateTime.UtcNow:mm}-test",
            //     Extension = ArchiveType.Pdf
            // };

            //var screenshotCreator = new ScreenshotCreator(mockEnvironment.Object);
            var screenshotCreator = new ScreenshotCreator();

            //var result = await screenshotCreator.TakeScreenshotAsync(archiveFile);

            //Assert.That(result, Is.EqualTo("TODO"));
        }
    }
}