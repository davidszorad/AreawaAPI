using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;
using Core.Shared;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using ArchiveType = Domain.Enums.ArchiveType;

namespace Core.Processor
{
    public class ProcessorService : IProcessorService
    {
        private readonly AreawaDbContext _areawaDbContext;
        private readonly IScreenshotCreator _screenshotCreator;
        private readonly IStorageService _storageService;
        private readonly HttpService _httpService;

        public ProcessorService(
            AreawaDbContext areawaDbContext,
            IScreenshotCreator screenshotCreator,
            IStorageService storageService)
        {
            _areawaDbContext = areawaDbContext;
            _screenshotCreator = screenshotCreator;
            _storageService = storageService;
            _httpService = new HttpService();
        }
        
        public async Task<(bool isSuccess, Status status)> ProcessAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            var websiteArchive = await _areawaDbContext.WebsiteArchive
                .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken);
            
            try
            {
                if (websiteArchive is not { EntityStatusId: Status.Pending })
                {
                    return (isSuccess: false, websiteArchive.EntityStatusId);
                }

                if (!await _httpService.IsStatusOkAsync(websiteArchive.SourceUrl, cancellationToken))
                {
                    await ChangeStatusAsync(websiteArchive, Status.SourceNotFound, cancellationToken);
                    return (isSuccess: false, Status.SourceNotFound);
                }
                
                await ChangeStatusAsync(websiteArchive, Status.Processing, cancellationToken);
                
                // TODO: print website to pdf/image
                var archiveFile = new ArchiveFile
                {
                    SourceUrl = websiteArchive.SourceUrl,
                    Extension = websiteArchive.ArchiveTypeId,
                    Folder = websiteArchive.ShortId,
                    Filename = websiteArchive.Name.Trim().Replace(" ", "-").ToLower()
                };
                var screenshotPath = await _screenshotCreator.CreateAsync(archiveFile, cancellationToken);
                await _storageService.CreateAsync(screenshotPath, archiveFile.Folder, archiveFile.Filename, cancellationToken);
                // TODO: upload printed result to storage
                
                await ChangeStatusAsync(websiteArchive, Status.Ok, cancellationToken);
                return (isSuccess: true, Status.Ok);
            }
            catch (Exception e)
            {
                await ChangeStatusAsync(websiteArchive, Status.Error, cancellationToken);
                
                // TODO: log error
                
                return (isSuccess: false, Status.Error);
            }
            
            // TODO: retrieve from queue (in Azure func)
            // TODO: remove from queue / add to poison queue / retry mechanism (in Azure func)
        }

        private async Task ChangeStatusAsync(WebsiteArchive websiteArchive, Status status, CancellationToken cancellationToken = default)
        {
            websiteArchive.EntityStatusId = status;
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
