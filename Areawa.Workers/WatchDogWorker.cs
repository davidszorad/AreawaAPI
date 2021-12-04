using System;
using System.Threading;
using System.Threading.Tasks;
using Core.WatchDog;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Areawa.Workers;

public class WatchDogWorker
{
    private readonly IWatchDogService _watchDogService;

    public WatchDogWorker(IWatchDogService watchDogService)
    {
        _watchDogService = watchDogService;
    }

    [Function("WatchDogWorkerProcessNew")]
    public async Task WatchDogWorkerProcessNewAsync([QueueTrigger("incoming-processor-requests", Connection = "AzureStorageConnectionString")] Guid queueItem, 
        int dequeueCount, ILogger logger, CancellationToken cancellationToken)
    {
        await _watchDogService.ProcessNewAsync(queueItem, cancellationToken);
    }
    
    // Function("WatchDogWorkerCheckChanges")]
    // public async Task WatchDogWorkerCheckChangesAsync([QueueTrigger("incoming-processor-requests", Connection = "AzureStorageConnectionString")] string queueItem, 
    //     int dequeueCount, ILogger logger, CancellationToken cancellationToken)
    // {
    //     await _watchDogService.CheckChangesAsync(cancellationToken);
    // }
}