using ECommerce.Domain.Abstraction;
using ECommerce.Inferastructure.Data;
using ECommerce.Inferastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure;

public static class DependacyInjection 
{
    public static IServiceCollection InferastructureServices( this IServiceCollection services , IConfiguration configuration)
    {
       services.AddConecttion(configuration); 
       services.AddRepository();
        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);

        });
       services.AddScoped<IBasketRepository, BasketRepository> ();
       
        return services;    
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    { 
        services.AddScoped<IUnitOfWork,UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddConecttion(this IServiceCollection services, IConfiguration configuration) {


        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    
    
        return services;    
    }
   
}
