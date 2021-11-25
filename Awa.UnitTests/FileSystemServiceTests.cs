using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Awa.UnitTests;

[TestFixture]
public class FileSystemServiceTests
{
    private static string _fileName = "testFile";
    
    [TearDown]
    public void TearDown()
    {
        FileSystemService.DeleteFile(_fileName);
    }
    
    [Test]
    public async Task SaveTextAsync_SaveNewGuid_GuidIsSavedAndCanBeRead()
    {
        var newGuid = Guid.NewGuid();

        await FileSystemService.SaveTextAsync(newGuid.ToString(), _fileName);
        var savedText = await FileSystemService.ReadTextAsync(_fileName);
        var readGuid = Guid.Parse(savedText);

        Assert.That(readGuid, Is.EqualTo(newGuid));
    }
}