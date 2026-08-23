using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.IdentityDto;

public record UserResultResponse(string DisplayName, string token, string Email)
{
}
