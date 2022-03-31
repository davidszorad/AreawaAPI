using System;
using System.Threading;
using System.Threading.Tasks;
using Core.WatchDogCreator;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Areawa.Workers;

public class WatchDogWorker
{
    private readonly IWatchDogCreatorService _watchDogCreatorService;

    public WatchDogWorker(IWatchDogCreatorService watchDogCreatorService)
    {
        _watchDogCreatorService = watchDogCreatorService;
    }

    [Function("WatchDogWorkerProcessNew")]
    public async Task WatchDogWorkerProcessNew(
        [QueueTrigger("watchdog-incoming", Connection = "AzureStorageConnectionString")] Guid queueItem, 
        int dequeueCount)
    {
        /*
         * [QueueTrigger("watchdog-incoming", Connection = "AzureStorageConnectionString")] Guid queueItem, 
         * int dequeueCount, ILogger logger, CancellationToken cancellationToken)
         */
        
        await _watchDogCreatorService.ProcessNewAsync(queueItem);
    }
    
    [Function("WatchDogWorkerCheckChanges")]
    public async Task WatchDogWorkerCheckChanges(
        [TimerTrigger("0 */15 * * * *")] TimerInfo timerInfo)
    {
        /*
         * [TimerTrigger("0 *~/5 * * * *")] TimerInfo timerInfo, 
         * FunctionContext context, CancellationToken cancellationToken)
         */
        
        //var logger = context.GetLogger("TimerFunction");
        //logger.LogInformation($"Function Ran. Next timer schedule = {timerInfo.ScheduleStatus.Next}");
        await _watchDogCreatorService.CheckChangesAsync();
    }
    
    [Function("WatchDogWorkerRetryFailed")]
    public async Task WatchDogWorkerRetryFailed(
        [TimerTrigger("0 0 12 * * *")] TimerInfo timerInfo)
    {
        await _watchDogCreatorService.RetryFailedAsync();
    }
}