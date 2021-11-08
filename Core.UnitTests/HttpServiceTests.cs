using Core.Shared;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Core.UnitTests
{
    [TestFixture]
    public class HttpServiceTests
    {
        private HttpService _httpService;

        [SetUp]
        public void Setup()
        {
            _httpService = new HttpService();
        }

        [Test]
        [TestCase("https://www.google.com")]
        [TestCase("https://www.hanselman.com/blog/dynamically-generating-robotstxt-for-aspnet-core-sites-based-on-environment")]
        public async Task IsStatusOkAsync_PageReturns200_ReturnsTrue(string url)
        {
            // Act
            var result = await _httpService.IsStatusOkAsync(url);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task IsStatusOkAsync_PageDoesntExistButWasHandledByWebsiteAndRedirected_ReturnsFalse()
        {
            // Arrange
            var url = "https://www.hanselman.com/blog/dynamically-generating-robotstxt-for-aspnet-core-sites-based-on-environment2";

            // Act
            var result = await _httpService.IsStatusOkAsync(url);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsStatusOkAsync_PageReturns404_ReturnsFalse()
        {
            // Arrange
            var url = "https://j1if2re7oj.com";

            // Act
            var result = await _httpService.IsStatusOkAsync(url);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }
    }
}
