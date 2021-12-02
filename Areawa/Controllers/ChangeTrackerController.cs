using System.Threading.Tasks;
using Core.ChangeTracker;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

public class ChangeTrackerController : ControllerBase
{
    private readonly IChangeTrackerService _changeTrackerService;
    private readonly IApiKeyValidator _apiKeyValidator;

    public ChangeTrackerController(
        IChangeTrackerService changeTrackerService,
        IApiKeyValidator apiKeyValidator)
    {
        _changeTrackerService = changeTrackerService;
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

        return Ok(await _changeTrackerService.GetSourcePreviewAsync(command));
    }
}