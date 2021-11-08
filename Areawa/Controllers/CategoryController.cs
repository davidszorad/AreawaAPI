using System;
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
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UpsertCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.CreateCategoryAsync(command));
        }
        
        [HttpPost("group/create")]
        public async Task<IActionResult> CreateGroup([FromBody] UpsertCategoryGroupCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.CreateCategoryGroupAsync(command));
        }
        
        [HttpPut("update/{publicId}")]
        public async Task<IActionResult> Update(Guid publicId, [FromBody] UpsertCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryAsync(publicId, command));
        }
        
        [HttpPut("group/update/{publicId}")]
        public async Task<IActionResult> UpdateGroup(Guid publicId, [FromBody] UpsertCategoryGroupCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryGroupAsync(publicId, command));
        }

        [HttpDelete("{publicId}")]
        public async Task<IActionResult> Delete([FromBody] Guid publicId)
        {
            await _categoriesService.DeleteCategoryAsync(publicId);
            
            return Ok();
        }
        
        [HttpDelete("group/{publicId}")]
        public async Task<IActionResult> DeleteGroup([FromBody] Guid publicId)
        {
            await _categoriesService.DeleteCategoryGroupAsync(publicId);
            
            return Ok();
        }
    }
}