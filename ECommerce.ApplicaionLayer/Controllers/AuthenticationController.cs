
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared.IdentityDto;

namespace ECommerce.ApplicaionLayer.Controllers;

[ApiController]
[Route("api/users")]
public class AuthenticationController(IserviceManger _serviceManger) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> login(UserloginRequest request)
    {
        return Ok(await _serviceManger.AuthenticationService.LoginAsync(request));
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterRequest request)
    {
        return Ok(await _serviceManger.AuthenticationService.RegisterAsync(request));

    }
}
