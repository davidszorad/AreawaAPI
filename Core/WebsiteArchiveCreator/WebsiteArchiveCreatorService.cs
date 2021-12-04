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
    private readonly IScreenshotCreator _screenshotCreator;
    private readonly IStorageService _storageService;
    private readonly IHttpService _httpService;

    public WebsiteArchiveCreatorService(
        AreawaDbContext areawaDbContext,
        IScreenshotCreator screenshotCreator,
        IStorageService storageService,
        IHttpService httpService)
    {
        _areawaDbContext = areawaDbContext;
        _screenshotCreator = screenshotCreator;
        _storageService = storageService;
        _httpService = httpService;
    }
    
    public async Task<(Status status, string shortId)> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, Stream stream, CancellationToken cancellationToken = default)
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
            EntityStatusId = Status.Ok,
            ApiUser = user
        };
        
        var archivePath = await _storageService.UploadAsync(stream, GetArchivePath(websiteArchive).folder, GetArchivePath(websiteArchive).filename, cancellationToken);
        websiteArchive.ArchiveUrl = archivePath;
        
        _areawaDbContext.WebsiteArchive.Add(websiteArchive);
        await _areawaDbContext.SaveChangesAsync(cancellationToken);
        
        return (websiteArchive.EntityStatusId, websiteArchive.ShortId);
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