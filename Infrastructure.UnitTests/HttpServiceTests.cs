using System.Threading.Tasks;
using NUnit.Framework;

namespace Infrastructure.UnitTests;

[TestFixture]
public class HttpServiceTests
{
    [Test]
    [TestCase("https://www.sme.sk")]
    public async Task GetHtmlBodyAsync_RetrievesHtmlBody_ReturnsHtmlOfProvidedUrl(string url)
    {
        var htmlService = new HttpService();
        var content = await htmlService.GetHtmlBodyAsync(url);
        
        Assert.That(content, Is.Not.Empty);
        Assert.That(content, Does.Contain("<HEAD>"));
    }
    
    [Test]
    [TestCase("text1 <script>text2</script> text3")]
    public async Task TestHtmlStripping(string htmlContent)
    {
        var strippedContent = htmlContent.StripScripts();
        
        Assert.That(strippedContent, Is.Not.Empty);
        Assert.That(strippedContent, Is.EqualTo("text1  text3"));
    }
}