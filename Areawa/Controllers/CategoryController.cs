using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Core.CategoriesManagement;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

[ApiController]
[Route("/api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    private readonly IApiKeyValidator _apiKeyValidator;

    public CategoryController(
        ICategoriesService categoriesService,
        IApiKeyValidator apiKeyValidator)
    {
        _categoriesService = categoriesService;
        _apiKeyValidator = apiKeyValidator;
    }

    [HttpGet]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Get()
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }

        return Ok(await _categoriesService.GetCategoriesAsync(apiKeyValidatorResult.userPublicId));
    }
        
    [HttpPost("create")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Create([FromBody] UpsertCategoryCommand command)
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

        return Ok(await _categoriesService.CreateCategoryAsync(apiKeyValidatorResult.userPublicId, command));
    }
        
    [HttpPost("group/create")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> CreateGroup([FromBody] UpsertCategoryGroupCommand command)
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

        return Ok(await _categoriesService.CreateCategoryGroupAsync(apiKeyValidatorResult.userPublicId, command));
    }
        
    [HttpPut("update/{publicId:guid}")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Update(Guid publicId, [FromBody] UpsertCategoryCommand command)
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

        return Ok(await _categoriesService.UpdateCategoryAsync(apiKeyValidatorResult.userPublicId, publicId, command));
    }
        
    [HttpPut("group/update/{publicId:guid}")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> UpdateGroup(Guid publicId, [FromBody] UpsertCategoryGroupCommand command)
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

        return Ok(await _categoriesService.UpdateCategoryGroupAsync(apiKeyValidatorResult.userPublicId, publicId, command));
    }

    [HttpDelete("{publicId:guid}")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> Delete([FromHeader(Name="X-Email")][Required] string userEmail, Guid publicId)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }
            
        return Ok(await _categoriesService.DeleteCategoryAsync(apiKeyValidatorResult.userPublicId, publicId));
    }
        
    [HttpDelete("group/{publicId:guid}")]
    [EnableCors("AreawaCorsPolicy")]
    public async Task<IActionResult> DeleteGroup([FromHeader(Name="X-Email")][Required] string userEmail, Guid publicId)
    {
        var apiKeyValidatorResult = await _apiKeyValidator.ValidateAsync(Request);
        if (!apiKeyValidatorResult.isValid)
        {
            return BadRequest();
        }
            
        return Ok(await _categoriesService.DeleteCategoryGroupAsync(apiKeyValidatorResult.userPublicId, publicId));
    }
}