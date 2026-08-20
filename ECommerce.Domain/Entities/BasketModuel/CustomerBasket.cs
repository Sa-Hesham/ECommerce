

namespace ECommerce.Domain.Entities.BasketModuel;

public class CustomerBasket
{
    public string Id { get; set; } = null!;

    public ICollection<BasketItem> BasketItem { get; set; } = [];
}
