using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.mapping;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<DeliveryMethod, DeliveryMethodResponse>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(ds => ds.ProductId, option => option.MapFrom(src => src.product.ProductId))
            .ForMember(ds => ds.ProductName, option => option.MapFrom(src => src.product.ProductName))
            .ForMember(ds => ds.PictureUrl, option => option.MapFrom(src => src.product.PictureUrl));

        CreateMap<Order, OrderResponse>()
            .ForMember(ds => ds.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
            .ForMember(ds => ds.OrderPaymentStatus, option => option.MapFrom(src => src.OrderPaymentStatus.ToString()))
            .ForMember(ds => ds.Total, option => option.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price ));
            
            
    }
}
