using Core.WatchDogCreator;
using NUnit.Framework;

namespace Core.UnitTests;

[TestFixture]
public class ChangeTrackerServiceTests
{
    [Test]
    [TestCase("text1<script>text2</script>text3")]
    public void TestHtmlStripping(string htmlContent)
    {
        var strippedContent = htmlContent.StripScripts();
        
        Assert.That(strippedContent, Is.Not.Empty);
        Assert.That(strippedContent, Is.EqualTo("text1text3"));
    }
}