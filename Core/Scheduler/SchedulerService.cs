using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Database;
using Core.Database.Entities;
using Core.Shared;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly AreawaDbContext _areawaDbContext;
        private readonly IQueueService _queueService;

        public SchedulerService(
            AreawaDbContext areawaDbContext,
            IQueueService queueService)
        {
            _areawaDbContext = areawaDbContext;
            _queueService = queueService;
        }

        public async Task<Guid> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, CancellationToken cancellationToken = default)
        {
            var user = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken: cancellationToken);
            
            var websiteArchiveEntity = new WebsiteArchive
            {
                Name = command.Name,
                Description = command.Description,
                SourceUrl = command.SourceUrl,
                ArchiveTypeId = command.ArchiveType,
                PublicId = Guid.NewGuid(),
                ShortId = ShortIdGenerator.Generate(),
                EntityStatusId = Status.Pending,
                ApiUser = user
            };

            _areawaDbContext.WebsiteArchive.Add(websiteArchiveEntity);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            await _queueService.InsertMessageAsync(
                ConfigurationConstants.QueueIncomingProcessorRequests,
                websiteArchiveEntity.PublicId.ToString(),
                cancellationToken);

            return websiteArchiveEntity.PublicId;
        }
    }
}
