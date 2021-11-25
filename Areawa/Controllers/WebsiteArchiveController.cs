using Areawa.Models;
using Core.Reader;
using Core.Scheduler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Areawa.Controllers;

[ApiController]
[Route("/api/website-archive")]
public class WebsiteArchiveController : ControllerBase
{
    private readonly ILogger<WebsiteArchiveController> _logger;
    private readonly IReaderService _readerService;
    private readonly ISchedulerService _schedulerService;
    private readonly IApiKeyValidator _apiKeyValidator;

    public WebsiteArchiveController(
        ILogger<WebsiteArchiveController> logger,
        IReaderService readerService,
        ISchedulerService schedulerService,
        IApiKeyValidator apiKeyValidator)
    {
        _logger = logger;
        _readerService = readerService;
        _schedulerService = schedulerService;
        _apiKeyValidator = apiKeyValidator;
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

        var result = await _readerService.GetAsync(filterQuery);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateArchivedWebsiteCommand createArchivedWebsiteCommand)
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

        return Ok(await _schedulerService.CreateAsync(createArchivedWebsiteCommand, apiKeyValidatorResult.userPublicId));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadScreenshot()
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        return Ok();
    }
}