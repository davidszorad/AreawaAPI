using System.Threading.Tasks;
using Configuration;
using NUnit.Framework;

namespace Infrastructure.UnitTests;

[TestFixture]
public class AzureStorageQueueServiceTests
{
    [Test]
    [TestCase("4FE15D57-3DD2-49B0-B4A0-ED026A4CED13")]
    //[TestCase("8C78F403-F119-4291-A60F-9B1FD7F7067D")]
    public async Task InsertMessageAsync_InsertGuidAsMsg_InsertsNewMsgToQueue(string message)
    {
        var queueService = new AzureStorageQueueService();
        Assert.That(async () => await queueService.InsertMessageAsync(ConfigurationConstants.QueueIncomingProcessorRequests, message), Throws.Nothing);
    }
}