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
        
        [HttpPut("update/{publicId:guid}")]
        public async Task<IActionResult> Update(Guid publicId, [FromBody] UpsertCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryAsync(publicId, command));
        }
        
        [HttpPut("group/update/{publicId:guid}")]
        public async Task<IActionResult> UpdateGroup(Guid publicId, [FromBody] UpsertCategoryGroupCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoriesService.UpdateCategoryGroupAsync(publicId, command));
        }

        [HttpDelete("{publicId:guid}")]
        public async Task<IActionResult> Delete(Guid publicId)
        {
            return Ok(await _categoriesService.DeleteCategoryAsync(publicId));
        }
        
        [HttpDelete("group/{publicId:guid}")]
        public async Task<IActionResult> DeleteGroup(Guid publicId)
        {
            return Ok(await _categoriesService.DeleteCategoryGroupAsync(publicId));
        }
    }
}