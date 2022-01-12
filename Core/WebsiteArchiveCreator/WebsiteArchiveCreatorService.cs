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
    private readonly AreawaDbContext _areawaDbContext;
    private readonly IStorageService _storageService;
    private readonly IHttpService _httpService;

    public WebsiteArchiveCreatorService(
        AreawaDbContext areawaDbContext,
        IStorageService storageService,
        IHttpService httpService)
    {
        _areawaDbContext = areawaDbContext;
        _storageService = storageService;
        _httpService = httpService;
    }
    
    public async Task<(Status status, string shortId)> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, CancellationToken cancellationToken = default)
    {
        if (!await _httpService.IsStatusOkAsync(command.SourceUrl, cancellationToken))
        {
            return (Status.SourceNotFound, string.Empty);
        }
        
        var user = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
        
        var websiteArchive = new WebsiteArchive
        {
            Name = command.Name,
            Description = command.Description,
            SourceUrl = command.SourceUrl,
            ArchiveTypeId = command.ArchiveType,
            PublicId = Guid.NewGuid(),
            ShortId = ShortIdGenerator.Generate(),
            EntityStatusId = Status.Processing,
            ApiUser = user,
            IsActive = true
        };
        
        _areawaDbContext.WebsiteArchive.Add(websiteArchive);
        await _areawaDbContext.SaveChangesAsync(cancellationToken);
        
        return (websiteArchive.EntityStatusId, websiteArchive.ShortId);
    }

    public async Task<(Status status, string shortId)> UploadAsync(string shortId, Stream stream, CancellationToken cancellationToken = default)
    {
        var websiteArchive = await _areawaDbContext.WebsiteArchive.SingleAsync(x => x.ShortId.Equals(shortId), cancellationToken);
        
        var archivePath = await _storageService.UploadAsync(stream, GetArchivePath(websiteArchive).folder, GetArchivePath(websiteArchive).filename, cancellationToken);
        websiteArchive.ArchiveUrl = archivePath;
        websiteArchive.EntityStatusId = Status.Ok;
        await _areawaDbContext.SaveChangesAsync(cancellationToken);
        
        return (websiteArchive.EntityStatusId, websiteArchive.ShortId);
    }

    public async Task DeactivateAsync(Guid publicId, Guid userPublicId, CancellationToken cancellationToken = default)
    {
        var user = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);

        var websiteArchive = await _areawaDbContext.WebsiteArchive.SingleAsync(x => 
            x.PublicId == publicId && 
            x.IsActive && 
            x.ApiUserId == user.ApiUserId, 
            cancellationToken);

        websiteArchive.IsActive = false;

        await _storageService.DeleteFolderAsync(GetArchivePath(websiteArchive).folder, cancellationToken);
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