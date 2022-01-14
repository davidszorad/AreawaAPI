using System;
using System.Threading.Tasks;
using Core.WatchDog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

[Route("/api/watchdog")]
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

        return Ok(await _watchDogService.GetSourcePreviewAsync(command));
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

        return Ok(await _watchDogService.PreviewAsync(command));
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

        return Ok(await _watchDogService.ScheduleAsync(command, apiKeyValidatorResult.userPublicId));
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

        await _watchDogService.DeleteAsync(publicId, apiKeyValidatorResult.userPublicId);
        return Ok();
    }
}