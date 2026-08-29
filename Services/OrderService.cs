using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.BasketModuel;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Exceptions;
using Services.Specfiactions;
using ServicesAbstraction.Contracts;
using Shared.Request;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class OrderService(IMapper _mapp, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
{
    public async Task<OrderResponse> CreateOrderAsync(OrderRequest request, string userEmail, CancellationToken ct = default)
    {
        var address = _mapp.Map<Address>(request.Address);
        var basket = await _basketRepository.GetBasketByIdAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);
        var OrderItems = new List<OrderItem>();
        foreach (var item in basket.BasketItem)
        {
            var product = await _unitOfWork.GetRepo<Product, int>().GettByIdAsync(item.Id)
                ?? throw new ProductNotFoundException(item.Id);

            OrderItems.Add(CreateOrderItem(item, product));

        }

        var Deliverymethod = await _unitOfWork.GetRepo<DeliveryMethod, int>().GettByIdAsync(request.DeliveryMethodId, ct)??
            throw new DelivermethodNotFoundException(request.DeliveryMethodId);

        var subTotal = OrderItems.Sum(item => item.Price * item.Quatity);

        var order = new Order(userEmail, address, OrderItems, Deliverymethod, subTotal,basket.PaymentIntentId??"");

         await _unitOfWork.GetRepo<Order,Guid>().Createasync(order,ct);
          await _unitOfWork.SaveChangesasync(ct);

        return _mapp.Map<OrderResponse>(order); 
    }

    private OrderItem CreateOrderItem(BasketItem item, Product product)
    {
        var ProductInitem = new ProductInOrderItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            PictureUrl = product.PictureUrl,

        };
        return new OrderItem(ProductInitem, product.price, item.Quantity);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync(CancellationToken ct = default)
    {
      var Entity =  await _unitOfWork.GetRepo<DeliveryMethod, int>().Getallasync(ct);

        return _mapp.Map<IEnumerable<DeliveryMethodResponse>>(Entity);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrderByEmailAsync(string userEmail, CancellationToken ct = default)
    {
        var specficatin = new OrderSpecfication(userEmail);
        var orders = await _unitOfWork.GetRepo<Order,Guid>().Getallasync(specficatin,ct);
        return _mapp.Map<IEnumerable<OrderResponse>>(orders);
    }

    public async Task<OrderResponse> GetOrderByIdAsync(Guid id, CancellationToken ct = default)
    {
        var specficatin = new OrderSpecfication(id);
        var order = await _unitOfWork.GetRepo<Order, Guid>().GettByIdAsync(specficatin, ct) 
            ?? throw new  OrderNotFoundException(id);    
        return _mapp.Map<OrderResponse>(order);
    }
}
