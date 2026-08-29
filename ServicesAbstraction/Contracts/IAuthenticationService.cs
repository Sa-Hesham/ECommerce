using Shared.IdentityDto;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction.Contracts;

public interface IAuthenticationService
{
    public Task<UserResultResponse> LoginAsync(UserloginRequest request);
    public Task<UserResultResponse> RegisterAsync(UserRegisterRequest request);

    public Task<UserResultResponse>GetCurrentuserAsync (string userEmail);

    public Task <bool> CheakEmailAddressAsync(string email);

    public Task<AddressDto> GetuserAddressAsync (string userEmail);  
    public Task<AddressDto> updateuserAddressAsync (string userEmail,AddressDto address);  
}
