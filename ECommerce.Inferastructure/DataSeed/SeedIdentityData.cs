using ECommerce.Domain.Entities.IdentityModel;
using ECommerce.Inferastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.DataSeed;

public class SeedIdentityData( UserManager<ApplicationUser> _user 
    , RoleManager<IdentityRole> _role ) : ISeedIdentityData
{
    public async Task SeedRoleAndUserData()
    {
        //Create roles 
        if (!_role.Roles.Any())
        {
            await _role.CreateAsync(new IdentityRole("Admin"));
            await _role.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        if (!_user.Users.Any())
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin", 
                DisplayName = "Admin",  
                Email = "Admin@Gmail.com",


            };

            var superAdmin = new ApplicationUser
            {
                UserName = "superAdmin",
                DisplayName = "SuperAdmin",
                Email = "SuperAdmin@Gmail.com",
            };

            await _user.CreateAsync(adminUser, "P@ssw0rd");
            await _user.AddToRoleAsync(adminUser, "Admin");
            await _user.CreateAsync(superAdmin, "P@ssw0rd1");
            await _user.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
}
