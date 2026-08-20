
using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities.BasketModuel;
using ECommerce.Domain.Exceptions;
using ServicesAbstraction.Contracts;
using Shared.Response;

namespace Services.BasketServices;

public class BasketService(IBasketRepository _repo
    , IMapper _map) : IBasketService
{
    public async Task<CustomerBasketResponse> CreatOrupdate(CustomerBasketResponse basket)
    {
        var Realmodel = _map.Map<CustomerBasket>(basket);

        var basketResponse =  await _repo.GreateOrUpdateAsync(Realmodel);
        if (basketResponse is null)
        {
            throw new Exception("Create or update Failed");
        } 
            

        return _map.Map<CustomerBasketResponse>(basketResponse);   
        
    }

    public async Task<bool> DeleteBasket(string id)
    {
         return await _repo.DeleteBasketAsync(id);  
    }

    public async Task<CustomerBasketResponse> GetBasketById(string id)
    {
          var basket = await _repo.GetBasketByIdAsync(id);
        if (basket is null)
            throw new BasketNotFoundException(id);

        return _map.Map<CustomerBasketResponse>(basket);
    }
}
