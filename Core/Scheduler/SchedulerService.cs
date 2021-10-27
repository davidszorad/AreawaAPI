using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        public async Task<Guid> CreateAsync(CreateArchivedWebsiteCommand command, CancellationToken cancellationToken = default)
        {
            // TODO: add to queue
            
            
            await Task.FromResult(0);

            return Guid.Empty;
        }
    }
}
