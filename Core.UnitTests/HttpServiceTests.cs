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
        public async Task IsStatusOkAsync_PageReturns200_ReturnsTrue()
        {
            // Arrange
            var liveUrl = "https://www.google.com";

            // Act
            var result = await _httpService.IsStatusOkAsync(liveUrl);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task IsStatusOkAsync_PageReturns404_ReturnsFalse()
        {
            // Arrange
            var deadUrl = "https://j1if2re7oj.com";

            // Act
            var result = await _httpService.IsStatusOkAsync(deadUrl);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }
    }
}
