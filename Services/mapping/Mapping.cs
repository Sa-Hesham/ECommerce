

using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.BasketModuel;
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
           .ForMember(ds => ds.TypeName, opt => opt.MapFrom(src => src.ProductType!.Name))
           .ForMember(ds=>ds.PictureUrl,opt=>opt.MapFrom<PictureResolver>());

        #region BasketMapping
        CreateMap<CustomerBasket, CustomerBasketResponse>()
            .ReverseMap();
        CreateMap<BasketItem, BasketItemsResponse>()
            .ReverseMap();
        #endregion
    }
}
