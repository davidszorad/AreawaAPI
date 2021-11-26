using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;
using Core.Shared;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using ArchiveType = Domain.Enums.ArchiveType;

namespace Core.WebsiteArchiveCreator;

public class WebsiteArchiveCreatorService : IWebsiteArchiveCreatorService
{
    private readonly HttpService _httpService;
    private readonly AreawaDbContext _areawaDbContext;
    private readonly IScreenshotCreator _screenshotCreator;
    private readonly IStorageService _storageService;

    public WebsiteArchiveCreatorService(
        AreawaDbContext areawaDbContext,
        IScreenshotCreator screenshotCreator,
        IStorageService storageService)
    {
        _httpService = new HttpService();
        _areawaDbContext = areawaDbContext;
        _screenshotCreator = screenshotCreator;
        _storageService = storageService;
    }
    
    public async Task<string> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, Stream stream, CancellationToken cancellationToken = default)
    {
        var user = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
        var websiteArchive = new WebsiteArchive
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

        _areawaDbContext.WebsiteArchive.Add(websiteArchive);
        await _areawaDbContext.SaveChangesAsync(cancellationToken);
        
        if (!await _httpService.IsStatusOkAsync(websiteArchive.SourceUrl, cancellationToken))
        {
            await ChangeStatusAsync(websiteArchive, Status.SourceNotFound, cancellationToken);
            return string.Empty;
        }
        
        await ChangeStatusAsync(websiteArchive, Status.Processing, cancellationToken);
                
        var archivePath = await _storageService.UploadAsync(stream, GetArchivePath(websiteArchive).folder, GetArchivePath(websiteArchive).filename, cancellationToken);
        await ChangeArchivePathAsync(websiteArchive, archivePath, cancellationToken);
        await ChangeStatusAsync(websiteArchive, Status.Ok, cancellationToken);
        
        return websiteArchive.ShortId;
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
    
    private (string folder, string filename) GetArchivePath(WebsiteArchive websiteArchive)
    {
        return websiteArchive.ArchiveTypeId switch
        {
            ArchiveType.Pdf => (folder: websiteArchive.ShortId.ToLower(), filename: $"{websiteArchive.Name.Trim().Replace(" ", "-").ToLower()}.pdf"),
            ArchiveType.Png => (folder: websiteArchive.ShortId.ToLower(), filename: $"{websiteArchive.Name.Trim().Replace(" ", "-").ToLower()}.png"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}