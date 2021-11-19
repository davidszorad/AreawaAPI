using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Processor;
using Core.Shared;
using Domain.Enums;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Areawa.Workers;

public class ProcessNewScreenshot
{
    private readonly IProcessorService _processorService;
    private readonly IPoisonQueueService _poisonQueueService;

    public ProcessNewScreenshot(
        IProcessorService processorService,
        IPoisonQueueService poisonQueueService)
    {
        _processorService = processorService;
        _poisonQueueService = poisonQueueService;
    }
    
    [Function("ProcessNewScreenshot")]
    public async Task ProcessNewScreenshotAsync([QueueTrigger("incoming-processor-requests", Connection = "AzureStorageConnectionString")]
        string queueItem, int dequeueCount)
    {
        var source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;
        
        //var logger = context.GetLogger("ProcessNewScreenshot");
        //logger.LogInformation($"C# Queue trigger function processed: {queueItem}");

        try
        {
            var result = await _processorService.ProcessAsync(Guid.Parse(queueItem), cancellationToken);
            if (!result.isSuccess)
            {
                if (result.status == Status.Ok)
                {
                    throw new Exception($"{ nameof(ProcessNewScreenshotAsync) }: Exception - Item was already processed");
                }
                throw new Exception($"{ nameof(ProcessNewScreenshotAsync) }: Exception - { result.status }");
            }
        }
        catch (Exception ex)
        {
            if (dequeueCount >= ConfigurationConstants.MaxDequeueCount)
            {
                await _poisonQueueService.InsertMessageAsync("incoming-processor-requests", queueItem, ex, cancellationToken);
            }
            throw;
        }
        
    }
}