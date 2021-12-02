using System.Threading.Tasks;
using NUnit.Framework;

namespace Infrastructure.UnitTests;

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
    [TestCase("https://www.sme.sk")]
    public async Task GetHtmlBodyAsync_RetrievesHtmlBody_ReturnsHtmlOfProvidedUrl(string url)
    {
        var content = await _httpService.GetHtmlSourceAsync(url);
        
        Assert.That(content, Is.Not.Empty);
        Assert.That(content, Does.Contain("<head>"));
    }
    
    [Test]
    [TestCase("https://www.google.com")]
    [TestCase("https://www.hanselman.com/blog/dynamically-generating-robotstxt-for-aspnet-core-sites-based-on-environment")]
    [TestCase("https://www.theguardian.com/world/2021/nov/08/palestinian-activists-mobile-phones-hacked-by-nso-says-report")]
    public async Task IsStatusOkAsync_PageReturns200_ReturnsTrue(string url)
    {
        // Act
        var result = await _httpService.IsStatusOkAsync(url);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public async Task IsStatusOkAsync_PageRedirectsToAnotherPageAndReturns302_ReturnsFalse()
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
        var url = "https://www.theguardian.com/world/2021/nov/08/palestinian-activists-mobile-phones-hacked-by-nso-says-report2";

        // Act
        var result = await _httpService.IsStatusOkAsync(url);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public async Task IsStatusOkAsync_FiresHostNotFoundException_ReturnsFalse()
    {
        // Arrange
        var url = "https://j1if2re7oj.com";

        // Act
        var result = await _httpService.IsStatusOkAsync(url);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }
}