

using AutoMapper;
using ECommerce.Domain.Entities;
using Shared.Response;

namespace Services.mapping;

public class Mapping :Profile
{
    public Mapping()
    {
        CreateMap<ProductType, productTypeResponse>();
        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<Product, ProductResponse>()
           .ForMember(ds => ds.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
           .ForMember(ds => ds.TypeName, opt => opt.MapFrom(src => src.ProductType!.Name));
           
    }
}
