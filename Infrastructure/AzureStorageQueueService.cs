using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Configuration;

namespace Infrastructure;

public class AzureStorageQueueService
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
    
    public async Task ReceiveMessageAsync(string queueName, CancellationToken cancellationToken = default)
    {
        var queueClient = CreateQueueClient(queueName);

        if (await queueClient.ExistsAsync(cancellationToken))
        { 
            QueueProperties properties = await queueClient.GetPropertiesAsync(cancellationToken);
            int cachedMessagesCount = properties.ApproximateMessagesCount;

            if (cachedMessagesCount > 20)
            {
                
            }
            
            QueueMessage[] retrievedMessage = await queueClient.ReceiveMessagesAsync(20, TimeSpan.FromMinutes(5), cancellationToken);

            var messageContent = retrievedMessage[0].Body.ToString();

            if (retrievedMessage[0].DequeueCount > 3)
            {
                await queueClient.DeleteMessageAsync(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt, cancellationToken);   
            }

            //return messageContent;
        }
    }
    
    private QueueClient CreateQueueClient(string queueName)
    {
        string connectionString = ConfigStore.GetValue(ConfigurationConstants.AzureStorageConnectionString);
        return new QueueClient(connectionString, queueName);
    }
}