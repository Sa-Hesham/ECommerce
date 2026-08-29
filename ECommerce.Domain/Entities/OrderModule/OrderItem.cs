using System.Globalization;

namespace ECommerce.Domain.Entities.OrderModule;

public class OrderItem :BaseEntity<Guid>
{
    public OrderItem(ProductInOrderItem product, decimal price, int quatity)
    {
        this.product = product;
        Price = price;
        Quatity = quatity;
    }
    public OrderItem()
    {
        
    }

    public ProductInOrderItem product { get; set; } = null!;
    public decimal Price { get; set; }  

    public int Quatity { get; set; }

    public Guid OrderId { get; set; }
   
}