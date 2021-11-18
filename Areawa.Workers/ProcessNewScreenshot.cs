using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Areawa.Workers;

public static class ProcessNewScreenshot
{
    [Function("ProcessNewScreenshot")]
    public static void Run([QueueTrigger("myqueue", Connection = "")] string myQueueItem, FunctionContext context)
    {
        var logger = context.GetLogger("ProcessNewScreenshot");
        logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        
    }
}