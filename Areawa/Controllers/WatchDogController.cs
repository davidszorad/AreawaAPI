using System;
using System.Threading.Tasks;
using Core.WatchDog;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

public class WatchDogController : ControllerBase
{
    private readonly IWatchDogService _watchDogService;
    private readonly IApiKeyValidator _apiKeyValidator;

    public WatchDogController(
        IWatchDogService watchDogService,
        IApiKeyValidator apiKeyValidator)
    {
        _watchDogService = watchDogService;
        _apiKeyValidator = apiKeyValidator;
    }
    
    [HttpPost("source-preview")]
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

        return Ok(await _watchDogService.GetSourcePreviewAsync(command));
    }

    [HttpPost("create")]
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

        return Ok(await _watchDogService.ScheduleAsync(command, apiKeyValidatorResult.userPublicId));
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        await _watchDogService.DeleteAsync(publicId, apiKeyValidatorResult.userPublicId);
        return Ok();
    }
}