using System.Threading.Tasks;
using NUnit.Framework;

namespace Infrastructure.UnitTests
{
    [TestFixture]
    public class ScreenshotCreatorTests
    {
        [Test]
        public async Task CreateAsync_ScreenshotIsGenerated_GeneratesPdfOutput()
        {
            var sourceUrl = "https://dev-trips.com/dev/core-unit-testing-techniques";

            var screenshotCreator = new ScreenshotCreator();

            var result = await screenshotCreator.CreateAsync(sourceUrl);

            Assert.That(result, Is.EqualTo("TODO"));
        }
    }
}