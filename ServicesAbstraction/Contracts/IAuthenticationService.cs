using Shared.IdentityDto;
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
}
