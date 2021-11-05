using Areawa.Models;
using Core.Reader;
using Core.Scheduler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Areawa.Controllers
{
    [ApiController]
    [Route("/api/website-archive")]
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

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] WebsiteArchiveQuery websiteArchiveQuery)
        {
            var filterQueryBuilder = new FilterQueryBuilder()
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _schedulerService.CreateAsync(createArchivedWebsiteCommand));
        }
    }
}
