using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Core.CategoriesManagement;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }

            return Ok(await _categoriesService.GetCategoriesAsync(userPublicId));
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UpsertCategoryCommand command)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.CreateCategoryAsync(userPublicId, command));
        }
        
        [HttpPost("group/create")]
        public async Task<IActionResult> CreateGroup([FromBody] UpsertCategoryGroupCommand command)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.CreateCategoryGroupAsync(userPublicId, command));
        }
        
        [HttpPut("update/{publicId:guid}")]
        public async Task<IActionResult> Update(Guid publicId, [FromBody] UpsertCategoryCommand command)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryAsync(userPublicId, publicId, command));
        }
        
        [HttpPut("group/update/{publicId:guid}")]
        public async Task<IActionResult> UpdateGroup(Guid publicId, [FromBody] UpsertCategoryGroupCommand command)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryGroupAsync(userPublicId, publicId, command));
        }

        [HttpDelete("{publicId:guid}")]
        public async Task<IActionResult> Delete([FromHeader(Name="X-Email")][Required] string userEmail, Guid publicId)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            return Ok(await _categoriesService.DeleteCategoryAsync(userPublicId, publicId));
        }
        
        [HttpDelete("group/{publicId:guid}")]
        public async Task<IActionResult> DeleteGroup([FromHeader(Name="X-Email")][Required] string userEmail, Guid publicId)
        {
            if (!HeaderParser.TryGetGetApiKey(Request, out var userPublicId))
            {
                return BadRequest();
            }
            
            return Ok(await _categoriesService.DeleteCategoryGroupAsync(userPublicId, publicId));
        }
    }
}