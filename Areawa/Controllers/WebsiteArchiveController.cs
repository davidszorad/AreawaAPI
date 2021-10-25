using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Reader;

namespace Areawa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebsiteArchiveController : ControllerBase
    {
        private readonly ILogger<WebsiteArchiveController> _logger;
        private readonly IReaderService _readerService;

        public WebsiteArchiveController(
            ILogger<WebsiteArchiveController> logger,
            IReaderService readerService)
        {
            _logger = logger;
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _readerService.GetAsync();
            return Ok(result);
        }
    }
}
