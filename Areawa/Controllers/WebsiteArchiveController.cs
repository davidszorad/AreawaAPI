using System;
using Areawa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Core.Shared;
using Core.WebsiteArchiveCreator;
using Core.WebsiteArchiveReader;
using Domain.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace Areawa.Controllers;

[ApiController]
[Route("/api/wa")]
public class WebsiteArchiveController : ControllerBase
{
    private readonly ILogger<WebsiteArchiveController> _logger;
    private readonly IWebsiteArchiveReaderService _websiteArchiveReaderService;
    private readonly IWebsiteArchiveCreatorService _websiteArchiveCreatorService;
    private readonly IApiKeyValidator _apiKeyValidator;
    private readonly IStorageService _storageService;

    public WebsiteArchiveController(
        ILogger<WebsiteArchiveController> logger,
        IWebsiteArchiveReaderService websiteArchiveReaderService,
        IWebsiteArchiveCreatorService websiteArchiveCreatorService,
        IApiKeyValidator apiKeyValidator,
        IStorageService storageService)
    {
        _logger = logger;
        _websiteArchiveReaderService = websiteArchiveReaderService;
        _websiteArchiveCreatorService = websiteArchiveCreatorService;
        _apiKeyValidator = apiKeyValidator;
        _storageService = storageService;
    }

    [HttpPost("search")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Search([FromBody] WebsiteArchiveQuery websiteArchiveQuery)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        var filterQueryBuilder = new FilterQueryBuilder()
            .SetUserPublicId(apiKeyValidatorResult.userPublicId)
            .OnlyActive()
            .SetOrdering(websiteArchiveQuery.SortBy, websiteArchiveQuery.IsSortDescending)
            .SetPaging(websiteArchiveQuery.Page, websiteArchiveQuery.PageSize);

        if (websiteArchiveQuery.PublicId.HasValue)
        {
            filterQueryBuilder.SetPublicId(websiteArchiveQuery.PublicId.Value);
        }

        if (websiteArchiveQuery.Status.HasValue)
        {
            filterQueryBuilder.SetStatus(websiteArchiveQuery.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(websiteArchiveQuery.ShortId))
        {
            filterQueryBuilder.SetShortId(websiteArchiveQuery.ShortId);
        }

        var filterQuery = filterQueryBuilder.Build();

        var result = await _websiteArchiveReaderService.GetAsync(filterQuery);
        return Ok(result);
    }

    [HttpPost("create")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> CreateWebsiteArchive([FromBody] CreateArchivedWebsiteCommand command)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _websiteArchiveCreatorService.CreateAsync(command, apiKeyValidatorResult.userPublicId);

        return result.status == Status.SourceNotFound ? 
            Problem("Source not found.") : 
            Ok(result.shortId);
    }
    
    [HttpPost("upload")]
    [EnableCors("AreawaCorsPolicy")]
    [RequestSizeLimit(20_000_000)] //default 30 MB (~28.6 MiB) max request body size limit -- https://github.com/aspnet/Announcements/issues/267
    public async Task<IActionResult> UploadScreenshot([FromQuery] string shortId, IFormFile file)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid || !Request.HasFormContentType)
        {
            return BadRequest();
        }
        
        if (string.IsNullOrWhiteSpace(shortId))
        {
            return BadRequest(shortId);
        }

        var screenshotStream = file.OpenReadStream();

        var result = await _websiteArchiveCreatorService.UploadAsync(shortId, screenshotStream);

        return result.status == Status.Ok ? 
            Ok($"Item created. ID: { result.shortId }") :
            Problem("An error happened.");
    }
    
    [HttpDelete("{publicId}")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Delete(Guid publicId)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        await _websiteArchiveCreatorService.DeactivateAsync(publicId, apiKeyValidatorResult.userPublicId);
        return Ok();
    }
}