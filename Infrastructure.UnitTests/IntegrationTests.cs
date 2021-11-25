using System;
using System.Threading.Tasks;
using Configuration;
using Core.Processor;
using Core.Shared;
using Domain.Enums;
using Domain.Models;
using NUnit.Framework;

namespace Infrastructure.UnitTests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    [TestCase("4FE15D57-3DD2-49B0-B4A0-ED026A4CED13")]
    //[TestCase("8C78F403-F119-4291-A60F-9B1FD7F7067D")]
    public async Task InsertMessageAsync_InsertGuidAsMsg_InsertsNewMsgToQueue(string message)
    {
        var queueService = new AzureStorageQueueService();
        Assert.That(async () => await queueService.InsertMessageAsync(ConfigurationConstants.QueueIncomingProcessorRequests, message), Throws.Nothing);
    }

    [Test]
    //[TestCase("devtrips-mediatr", "2021-11-04-21-19-TZSH", "https://dev-trips.com/dev/mediator-pattern-with-mediatr-in-net-core")]
    public async Task ProcessAsync_ProcessesGuidAsIfFromQueue_GeneratesAndUploadsPrintedWebsiteToBlob(string filename, string folder, string url)
    {
        var screenshotCreator = new ScreenshotCreator();
        var storageService = new AzureBlobStorageService();

        var archiveFile = new ArchiveFile
        {
            Filename = filename,
            Folder = folder,
            SourceUrl = url,
            Extension = ArchiveType.Pdf
        };
        
        var screenshotStream = await screenshotCreator.TakeScreenshotStreamAsync(archiveFile);
        var archivePath = await storageService.UploadAsync(screenshotStream, archiveFile.Folder, archiveFile.GetFilenameWithExtension());
        
        Assert.That(archivePath, Is.Not.Null);
    }
}