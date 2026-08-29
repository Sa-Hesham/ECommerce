using ECommerce.Domain.Entities.BasketModuel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstraction;

public interface IBasketRepository
{
    public Task<CustomerBasket?> GetBasketByIdAsync(string id);


    public Task<CustomerBasket?> GreateOrUpdateAsync (CustomerBasket Basket);


    public Task<bool> DeleteBasketAsync(string id);
}
