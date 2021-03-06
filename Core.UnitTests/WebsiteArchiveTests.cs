using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;
using Core.Shared;
using Core.UnitTests.Utils;
using Core.WebsiteArchiveCreator;
using Core.WebsiteArchiveReader;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ArchiveType = Domain.Enums.ArchiveType;

namespace Core.UnitTests;

[TestFixture]
public class WebsiteArchiveTests
{
    private readonly Mock<IStorageService> _mockStorageService;
    private readonly Mock<IHttpService> _mockHttpService;
    private AreawaDbContext _dbContext;
    
    public WebsiteArchiveTests()
    {
        _mockStorageService = new Mock<IStorageService>();
        _mockHttpService = new Mock<IHttpService>();
        _dbContext = null;
    }

    [SetUp]
    public void SetUp()
    {
        _dbContext = DbContextFactory.CreateInMemory();
        
        _mockStorageService
            .Setup(m => m.UploadAsync(It.IsAny<Stream>(), It.IsAny<ArchiveType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult("test"));
        _mockStorageService
            .Setup(m => m.DeleteFolderAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
        
        _mockHttpService
            .Setup(m => m.IsStatusOkAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));
    }
    
    [Test]
    public async Task WebsiteArchiveCreatorService_CreateAsync_WebsiteArchiveReaderService_GetAsync_CreatesAndReadsData()
    {
        IWebsiteArchiveCreatorService websiteArchiveCreatorService = new WebsiteArchiveCreatorService(
            _dbContext,
            _mockStorageService.Object,
            _mockHttpService.Object);

        IWebsiteArchiveReaderService websiteArchiveReaderService = new WebsiteArchiveReaderService(_dbContext);

        var command = new CreateArchivedWebsiteCommand
        {
            Name = "Test Name",
            Description = "Test Description",
            SourceUrl = "https://www.google.com",
            ArchiveType = ArchiveType.Pdf
        };
        
        _dbContext.ApiUser.Add(new ApiUser { PublicId = Guid.NewGuid(), FirstName = "Test", LastName = "Test", Email = "test@test.com", IsActive = true, IsPremium = true });
        await _dbContext.SaveChangesAsync();
        var userPublicId = (await _dbContext.ApiUser.Where(x => x.Email.Equals("test@test.com")).SingleAsync()).PublicId;

        var creatorResult = await websiteArchiveCreatorService.CreateAsync(command, userPublicId);
        
        var filterQuery = new FilterQueryBuilder()
            .SetUserPublicId(userPublicId)
            .SetShortId(creatorResult.shortId)
            .Build();
        var readerResult = await websiteArchiveReaderService.GetAsync(filterQuery);
        
        Assert.That(creatorResult.status, Is.EqualTo(Status.Processing));
        Assert.That(readerResult.Items.Count(), Is.EqualTo(1));
        Assert.That(readerResult.Items.First().Name, Is.EqualTo(command.Name));
        Assert.That(readerResult.Items.First().Description, Is.EqualTo(command.Description));
        Assert.That(readerResult.Items.First().SourceUrl, Is.EqualTo(command.SourceUrl));
        Assert.That(readerResult.Items.First().ArchiveType, Is.EqualTo(command.ArchiveType));
    }
}