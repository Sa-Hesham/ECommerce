using Shared.Response;

namespace ServicesAbstraction.Contracts;

public interface IBasketService
{
    public Task<CustomerBasketResponse> CreatOrupdate(CustomerBasketResponse basket);


    public Task<CustomerBasketResponse> GetBasketById(string id);



    public Task<bool> DeleteBasket(string id);
}
