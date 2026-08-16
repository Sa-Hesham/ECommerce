


using Microsoft.Extensions.DependencyInjection;
using Services.Products;
using Services.ServiceManger;
using ServicesAbstraction.Contracts;

namespace Services;

public  static class Dpendacyinjection
{

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
        services.addMapper();
        services.Services();
        
            return services;
        }


        private static IServiceCollection addMapper(this IServiceCollection services)
        {

        services.AddAutoMapper(typeof(AssemblyReferance).Assembly);

        return services;
        }
    private static IServiceCollection Services(this IServiceCollection services)
    {
        
        services.AddScoped<IserviceManger, ServicesManger>();

        return services;
    }
}


