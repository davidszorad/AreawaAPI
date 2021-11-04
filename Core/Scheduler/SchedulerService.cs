using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;

namespace Core.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly AreawaDbContext _areawaDbContext;

        public SchedulerService(AreawaDbContext areawaDbContext)
        {
            _areawaDbContext = areawaDbContext;
        }
        
        public async Task<Guid> CreateAsync(CreateArchivedWebsiteCommand command, CancellationToken cancellationToken = default)
        {
            
            
            
            
            _areawaDbContext.WebsiteArchive.Add()
            
            
            // TODO: add to queue
            
            
            await Task.FromResult(0);

            return Guid.Empty;
        }
    }
}
