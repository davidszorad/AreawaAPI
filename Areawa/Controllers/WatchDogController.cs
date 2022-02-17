using System;
using System.Threading.Tasks;
using Areawa.Models;
using Core.WatchDogCreator;
using Core.WatchDogReader;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

[Route("/api/watchdog")]
public class WatchDogController : ControllerBase
{
    private readonly IWatchDogCreatorService _watchDogCreatorService;
    private readonly IWatchDogReaderService _watchDogReaderService;
    private readonly IApiKeyValidator _apiKeyValidator;

    public WatchDogController(
        IWatchDogCreatorService watchDogCreatorService,
        IWatchDogReaderService watchDogReaderService,
        IApiKeyValidator apiKeyValidator)
    {
        _watchDogCreatorService = watchDogCreatorService;
        _watchDogReaderService = watchDogReaderService;
        _apiKeyValidator = apiKeyValidator;
    }
    
    [HttpPost("search")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Search([FromBody] WatchDogQuery watchDogQuery)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        var filterQueryBuilder = new FilterQueryBuilder()
            .SetUserPublicId(apiKeyValidatorResult.userPublicId)
            .SetOrdering(watchDogQuery.SortBy, watchDogQuery.IsSortDescending)
            .SetPaging(watchDogQuery.Page, watchDogQuery.PageSize);

        if (watchDogQuery.PublicId.HasValue)
        {
            filterQueryBuilder.SetPublicId(watchDogQuery.PublicId.Value);
        }

        if (watchDogQuery.Status.HasValue)
        {
            filterQueryBuilder.SetStatus(watchDogQuery.Status.Value);
        }
        
        if (watchDogQuery.IncludeInactive.HasValue && watchDogQuery.IncludeInactive.Value)
        {
            filterQueryBuilder.SetIncludeInactive();
        }

        var filterQuery = filterQueryBuilder.Build();

        var result = await _watchDogReaderService.GetAsync(filterQuery);
        return Ok(result);
    }
    
    [HttpPost("preview/source")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> GetSourcePreview([FromBody] SourcePreviewCommand command)
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

        return Ok(await _watchDogCreatorService.GetSourcePreviewAsync(command));
    }
    
    [HttpPost("preview/create")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> CreatePreview([FromBody] CreateWatchDogCommand command)
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

        return Ok(await _watchDogCreatorService.PreviewAsync(command));
    }

    [HttpPost("create")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Create([FromBody] CreateWatchDogCommand command)
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

        return Ok(await _watchDogCreatorService.ScheduleAsync(command, apiKeyValidatorResult.userPublicId));
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

        await _watchDogCreatorService.DeleteAsync(publicId, apiKeyValidatorResult.userPublicId);
        return Ok();
    }
}