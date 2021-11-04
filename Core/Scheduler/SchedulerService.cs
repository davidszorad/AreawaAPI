using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;
using EntityStatus = Core.Database.Entities.Enums.EntityStatus;

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
            var websiteArchiveEntity = new WebsiteArchive
            {
                Name = command.Name,
                Description = command.Description,
                SourceUrl = command.SourceUrl,
                ArchiveTypeId = command.ArchiveType,
                PublicId = Guid.NewGuid(),
                ShortId = ShortIdGenerator.Generate(),
                EntityStatusId = EntityStatus.Pending
            };

            _areawaDbContext.WebsiteArchive.Add(websiteArchiveEntity);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
            
            return websiteArchiveEntity.PublicId;
        }
    }
}
