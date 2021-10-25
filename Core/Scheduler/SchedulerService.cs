using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        public async Task<Guid> CreateAsync(ArchivedWebsite command, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);

            return Guid.Empty;
        }
    }
}
