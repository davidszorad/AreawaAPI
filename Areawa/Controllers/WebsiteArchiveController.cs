using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Reader;
using Core.Scheduler;

namespace Areawa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebsiteArchiveController : ControllerBase
    {
        private readonly ILogger<WebsiteArchiveController> _logger;
        private readonly IReaderService _readerService;
        private readonly ISchedulerService _schedulerService;

        public WebsiteArchiveController(
            ILogger<WebsiteArchiveController> logger,
            IReaderService readerService,
            ISchedulerService schedulerService)
        {
            _logger = logger;
            _readerService = readerService;
            _schedulerService = schedulerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _readerService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArchivedWebsiteCommand createArchivedWebsiteCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _schedulerService.CreateAsync(createArchivedWebsiteCommand).ConfigureAwait(false));
        }
    }
}
