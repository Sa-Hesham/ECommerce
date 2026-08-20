

using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string id) :base($"The Basket with {id} is Not found") 
    {
        
    }
}
