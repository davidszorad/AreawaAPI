using Areawa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Core.Shared;
using Core.WebsiteArchiveCreator;
using Core.WebsiteArchiveReader;
using Microsoft.AspNetCore.Http;

namespace Areawa.Controllers;

[ApiController]
[Route("/api/website-archive")]
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
    public async Task<IActionResult> Search([FromBody] WebsiteArchiveQuery websiteArchiveQuery)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }
            
        var filterQueryBuilder = new FilterQueryBuilder()
            .SetUserPublicId(apiKeyValidatorResult.userPublicId)
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
    [RequestSizeLimit(20_000_000)] //default 30 MB (~28.6 MiB) max request body size limit -- https://github.com/aspnet/Announcements/issues/267
    public async Task<IActionResult> UploadScreenshot([FromQuery] CreateArchivedWebsiteCommand command, IFormFile file)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid || !Request.HasFormContentType)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var screenshotStream = file.OpenReadStream();

        var resultId = await _websiteArchiveCreatorService.CreateAsync(command, apiKeyValidatorResult.userPublicId, screenshotStream);

        return Ok($"Item created. ID: {resultId}");
    }
}