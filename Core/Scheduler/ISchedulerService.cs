using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Scheduler
{
    public interface ISchedulerService
    {
        Task<Guid> CreateAsync(ArchivedWebsite command, CancellationToken cancellationToken = default);
    }
}
