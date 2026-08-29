using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction.Contracts;
using Shared.Response;
using Stripe;
using System.Data;


namespace Services.Payment;

internal class PaymentService(IConfiguration _config 
    ,IBasketRepository _basket
    ,IUnitOfWork _unitofwork , IMapper _mapper) : IPaymentService
{
    public async Task<CustomerBasketResponse> GetorupdatePaymentIntentAsync(string basketId)
    {  // 1. install stripe.net

        // 2. Set up key [=> stripe key]
        StripeConfiguration.ApiKey = _config.GetSection("StrpeSettings")["SecretKey"];
      
        // 3. Validate items price [=> (basket,item,price = product,from db) => product from db]
        var basket = await _basket.GetBasketByIdAsync(basketId)?? throw new BasketNotFoundException(basketId);
        foreach (var item in basket.BasketItem) {

            var product = await _unitofwork.GetRepo<ECommerce.Domain.Entities.Product, int>().GettByIdAsync(item.Id)
                ??throw new ProductNotFoundException(item.Id);
            item.Price = product.price;
        }

        // 4. Validate shipping price [=> get deliveryMethod [DeliveryMethodId] => shippingPrice = DeliveryMethod.Price]    
        if (!basket.DeliveryMethodId.HasValue)
            throw new Exception("No deliver method selected");
        var delivery = await _unitofwork.GetRepo<DeliveryMethod, int>().GettByIdAsync(basket.DeliveryMethodId.Value)
            ?? throw new DelivermethodNotFoundException(basket.DeliveryMethodId.Value);

        basket.ShippingPrice = delivery.Price;

        // 5. Total [=> (SubTotal + basket.items.price) + shippingPrice [DeliveryMethod.Price]]

        var total = (long)(basket.BasketItem.Sum(i => i.Quantity * i.Price ) + basket.ShippingPrice)*100;

        var stirpeService = new PaymentIntentService();
       if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            //create 
            var option = new PaymentIntentCreateOptions() { 
            
                Amount= total,  
                Currency="USD" ,
                PaymentMethodTypes = ["Card"]
            
            
            
            };
           var paymentIntent=  await stirpeService.CreateAsync(option);
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret= paymentIntent.ClientSecret;    
        }
        else
        {
            //update
            var payment = new PaymentIntentUpdateOptions()
            {
                Amount = total,
            };
            await stirpeService.UpdateAsync(basket.Id,payment);
        }

       //Createor update basket 
       await _basket.GreateOrUpdateAsync(basket);

        //mapp to CustomerbaskerRespnse 

        return _mapper.Map<CustomerBasketResponse>(basket);
    }
}
