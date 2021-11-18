using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Processor;
using Core.Shared;
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
        string queueItem, int dequeueCount, FunctionContext context, CancellationToken cancellationToken = default)
    {
        var logger = context.GetLogger("ProcessNewScreenshot");
        logger.LogInformation($"C# Queue trigger function processed: {queueItem}");

        try
        {
            var result = await _processorService.ProcessAsync(Guid.Parse(queueItem), cancellationToken);
            if (!result.isSuccess)
            {
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