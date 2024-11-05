using Hotelmanagment.Api.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hotelmanagment.Api.Contracts;

[Route("api/[controller]")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly IAuthManager _authManager;

    public AccountController(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost()]
    [Route("Register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] ApiUserDTO request)
    {
        var errors =await _authManager.Register(request);
        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok();

    }
    
    [HttpPost()]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var authResponse =await _authManager.Login(request);
        if (authResponse is null)
        {
            return Unauthorized();
        }

        return Ok(authResponse); 

    }
}

