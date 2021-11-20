using System.Threading.Tasks;
using Core.User;
using Microsoft.AspNetCore.Mvc;

namespace Areawa.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _userService.CreateAsync(command));
    }
}