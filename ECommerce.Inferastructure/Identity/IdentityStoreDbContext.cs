using ECommerce.Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.Identity;

public class IdentityStoreDbContext :IdentityDbContext
{
    public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> Identity):base(Identity)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Address>()
            .ToTable("addresses");
    }
}
