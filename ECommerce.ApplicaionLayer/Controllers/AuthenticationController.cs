
using ECommerce.Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared.IdentityDto;
using Shared.Response;
using System.Security.Claims;

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

    [HttpGet("EmailExist")]
    public async Task<bool>CheakEmailExistAsync(string Email)
    {
        return await _serviceManger.AuthenticationService.CheakEmailAddressAsync(Email);    
    }

    [HttpGet()]
    [Authorize]
    public async Task<ActionResult<UserResultResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await _serviceManger.AuthenticationService.GetCurrentuserAsync(email!));
    }

    [HttpGet("Address")]
    [Authorize]

    public async Task<ActionResult<AddressDto>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await _serviceManger.AuthenticationService.GetuserAddressAsync((email!));
        return Ok(result);  
    }

    [HttpPut("Address")]
    [Authorize]

    public async Task<ActionResult<AddressDto>> EditUserAddress (AddressDto address)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok( await _serviceManger.AuthenticationService.updateuserAddressAsync(email!, address));
    }
}
