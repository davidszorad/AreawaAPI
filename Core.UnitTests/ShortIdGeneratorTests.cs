using System;
using Core.WebsiteArchiveCreator;
using NUnit.Framework;

namespace Core.UnitTests;

[TestFixture]
public class ShortIdGeneratorTests
{
    [Test]
    public void Generate_ShortIdIsGenerated_ReturnsNewShortId()
    {
        var shortId = ShortIdGenerator.Generate();

        Assert.That(shortId, Does.StartWith($"{DateTime.UtcNow.Year}-{DateTime.UtcNow:MM}-{DateTime.UtcNow:dd}-{DateTime.UtcNow:HH}-{DateTime.UtcNow:mm}-"));
        Assert.That(shortId, Has.Length.EqualTo(21));
    }
}