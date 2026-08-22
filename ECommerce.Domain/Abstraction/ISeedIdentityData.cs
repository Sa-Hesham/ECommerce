using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstraction;

public interface ISeedIdentityData
{
    public Task SeedRoleAndUserData();
}
