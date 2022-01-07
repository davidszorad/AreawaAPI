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
    public async Task WatchDogWorkerProcessNew(
        [QueueTrigger("watchdog-incoming", Connection = "AzureStorageConnectionString")] Guid queueItem, 
        int dequeueCount)
    {
        /*
         * [QueueTrigger("watchdog-incoming", Connection = "AzureStorageConnectionString")] Guid queueItem, 
         * int dequeueCount, ILogger logger, CancellationToken cancellationToken)
         */
        
        await _watchDogService.ProcessNewAsync(queueItem);
    }
    
    [Function("WatchDogWorkerCheckChanges")]
    public async Task WatchDogWorkerCheckChanges(
        [TimerTrigger("0 */5 * * * *")] TimerInfo timerInfo)
    {
        /*
         * [TimerTrigger("0 *~/5 * * * *")] TimerInfo timerInfo, 
         * FunctionContext context, CancellationToken cancellationToken)
         */
        
        //var logger = context.GetLogger("TimerFunction");
        //logger.LogInformation($"Function Ran. Next timer schedule = {timerInfo.ScheduleStatus.Next}");
        await _watchDogService.CheckChangesAsync();
    }
}