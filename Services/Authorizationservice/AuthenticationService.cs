using ECommerce.Domain.Entities.IdentityModel;
using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction.Contracts;
using Shared.IdentityDto;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authorizationservice;

public class AuthenticationService(UserManager<ApplicationUser>_user , IConfiguration _config) : IAuthenticationService
{
    public async Task<bool> CheakEmailAddressAsync(string email)
    {
       var user =  await _user.FindByEmailAsync(email);
        if (user != null)
            return true;
        return false;
    }

    public async Task<UserResultResponse> GetCurrentuserAsync(string userEmail)
    {
        var user = await _user.FindByEmailAsync(userEmail);
        if (user == null)
            throw new USerNotFoundExseption(userEmail);

        return new UserResultResponse(user.DisplayName, await genrateTokenAsyc(user), user.Email!);
    }

    public async Task<AddressDto> GetuserAddressAsync(string userEmail)
    {
        var user = await _user.Users.Include(u=>u.Address)
            .FirstOrDefaultAsync(u=>u.Email==userEmail);
        if (user == null)
            throw new USerNotFoundExseption(userEmail);

        return new AddressDto
        {
            FirstName = user.Address?.FirstName ?? string.Empty,  
            LasttName = user.Address?.LastName ?? string.Empty, 
            Street= user.Address?.Street ?? string.Empty,   
            City= user.Address?.City ?? string.Empty,   
            Country=  user.Address?.Country ?? string.Empty,  


        };
    }

    public async Task<UserResultResponse> LoginAsync(UserloginRequest request)
    {
       ApplicationUser ? user= await _user.FindByEmailAsync(request.Email);
        if (user == null) {

            throw new AuthorizetionException();


        }

        var ValidPassword = await _user.CheckPasswordAsync(user, request.Password);
        if (!ValidPassword)
        {
            throw new AuthorizetionException();
        }
        return new UserResultResponse(user.DisplayName, await genrateTokenAsyc(user), user.Email!);




    }

    public async Task<UserResultResponse> RegisterAsync(UserRegisterRequest request)
    {
      var user =  await _user.FindByEmailAsync(request.Email);
        if(user is not null)
        {
            throw new ValidationException("invalid Email the Email is Exist");
        }

        var applicationUser = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            DisplayName = request.DisplayName,

        };

        var result = await _user.CreateAsync(applicationUser, request.Password);
        if (!result.Succeeded) {

            string massege = string.Join(",", result.Errors.Select(e => e.Description));
            throw new ValidationException(massege);
        
        }
        return new UserResultResponse(applicationUser.DisplayName,await genrateTokenAsyc(applicationUser), applicationUser.Email!);
    }

    public async Task<AddressDto> updateuserAddressAsync(string userEmail, AddressDto address)
    {
        var user = await _user.Users.Include(u => u.Address)
             .FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null)
            throw new USerNotFoundExseption(userEmail);
        if(user.Address != null)
        {
            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LasttName;
            user.Address.Street = address.Street;
            user.Address.City = address.City;   
            user.Address.Country = address.Country; 
        }

        var useraddress = new Address
        {
            FirstName = address.FirstName,
            LastName= address.LasttName,
            Street = address.Street,
            City = address.City,
            Country = address.Country,

        };
        user.Address= useraddress;  
       await  _user.UpdateAsync(user);
        return address;
    }

    private async Task<string> genrateTokenAsyc( ApplicationUser user )
    {
        var Jwt = _config.GetSection("JWT");
        var issuer = Jwt["Issuer"];
        var audiance = Jwt["Audience"];
        var key = Jwt["Key"];
        var Expiration = DateTime.UtcNow.AddMinutes( int.Parse(Jwt["TokenExpirationInMinutes"]!));


        List<Claim> claims = new()
        {
            new Claim (ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name , user.DisplayName!),
            new Claim(ClaimTypes.Email,user.Email!),

        };

        foreach( var role in await  _user.GetRolesAsync(user))
        {
            claims.Add( new Claim(ClaimTypes.Role,role));
        }


        var tokenDercriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Audience =audiance,
            Expires = Expiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
            ,SecurityAlgorithms.HmacSha256)

        };


        var token = new JwtSecurityTokenHandler();
        var securitytoken = token.CreateJwtSecurityToken(tokenDercriptor);
       var  AccessToken = token.WriteToken(securitytoken);

        return AccessToken;
    }
}
