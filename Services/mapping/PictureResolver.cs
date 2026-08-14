
using AutoMapper;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.Response;

namespace Services.mapping;

internal class PictureResolver(IConfiguration config) : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
      if(string.IsNullOrEmpty(source.PictureUrl) )
            return string.Empty;

        return $"{config.GetSection("URLs")["BaseURL"]}{source.PictureUrl}";
    }
}
