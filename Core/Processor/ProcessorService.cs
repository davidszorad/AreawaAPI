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
                
                var archiveFile = new ArchiveFile
                {
                    SourceUrl = websiteArchive.SourceUrl,
                    Extension = websiteArchive.ArchiveTypeId,
                    Folder = websiteArchive.ShortId,
                    Filename = websiteArchive.Name.Trim().Replace(" ", "-").ToLower()
                };
                //var screenshotPath = await _screenshotCreator.TakeScreenshotAsync(archiveFile, cancellationToken);
                //var archivePath = await _storageService.UploadAsync(screenshotPath, archiveFile.Folder, archiveFile.Filename, cancellationToken);
                var screenshotStream = await _screenshotCreator.TakeScreenshotStreamAsync(archiveFile, cancellationToken);
                var archivePath = await _storageService.UploadAsync(screenshotStream, archiveFile.Folder, archiveFile.Filename, cancellationToken);
                await ChangeArchivePathAsync(websiteArchive, archivePath, cancellationToken);
                await ChangeStatusAsync(websiteArchive, Status.Ok, cancellationToken);
                //_screenshotCreator.Cleanup(screenshotPath);
                return (isSuccess: true, Status.Ok);
            }
            catch (Exception)
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
        
        private async Task ChangeArchivePathAsync(WebsiteArchive websiteArchive, string archivePath, CancellationToken cancellationToken = default)
        {
            websiteArchive.ArchiveUrl = archivePath;
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
