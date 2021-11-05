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

namespace Core.Processor
{
    public class ProcessorService : IProcessorService
    {
        private readonly AreawaDbContext _areawaDbContext;
        private readonly HttpService _httpService;

        public ProcessorService(AreawaDbContext areawaDbContext)
        {
            _areawaDbContext = areawaDbContext;
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
