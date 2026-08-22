

using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities.IdentityModel;

public class ApplicationUser :IdentityUser
{
    public string DisplayName { get; set; } = null!;

    public Address ?Address { get; set;  }

}
