using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Configuration;
using Core.Shared;

namespace Infrastructure;

public class AzureStorageQueueService : IQueueService
{
    public async Task InsertMessageAsync(string queueName, string message, CancellationToken cancellationToken = default)
    {
        var queueClient = CreateQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        if (await queueClient.ExistsAsync(cancellationToken))
        {
            await queueClient.SendMessageAsync(message, cancellationToken);
        }
    }
    
    private QueueClient CreateQueueClient(string queueName)
    {
        string connectionString = ConfigStore.GetValue(ConfigurationConstants.AzureStorageConnectionString);
        return new QueueClient(connectionString, queueName, new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        });;
    }
}