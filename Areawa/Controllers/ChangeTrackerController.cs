using System.Threading.Tasks;
using Core.ChangeTracker;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

public class ChangeTrackerController : ControllerBase
{
    private readonly IChangeTrackerService _changeTrackerService;

    public ChangeTrackerController(IChangeTrackerService changeTrackerService)
    {
        _changeTrackerService = changeTrackerService;
    }
    
    [HttpPost("source-preview")]
    public async Task<IActionResult> GetSourcePreview([FromBody] SourcePreviewCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _changeTrackerService.GetSourcePreviewAsync(command));
    }
}