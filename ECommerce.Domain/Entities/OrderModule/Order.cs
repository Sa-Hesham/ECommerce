using System;
using System.Collections.Generic;
using ShippingAddress = ECommerce.Domain.Entities.OrderModule.Address;
    
namespace ECommerce.Domain.Entities.OrderModule;

public class Order :BaseEntity<Guid>
{
    public Order()
    {
        
    }

    public Order(string userEmail, ShippingAddress address, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal ,string paymentIntentId)
    {
        UserEmail = userEmail;
        Address = address;
        OrderItems = orderItems;
        DeliveryMethod = deliveryMethod;
        SubTotal = subTotal;
        PaymentIntentId=paymentIntentId;
    }

    public string UserEmail { get; set; } = null!;
    public ShippingAddress Address { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public OrderPaymentStatus OrderPaymentStatus {  get; set; } = OrderPaymentStatus.Pending;


    public DeliveryMethod DeliveryMethod { get; set; } = null!; 
    public int? DeliveryMethodId { get; set; }   

    public decimal SubTotal {  get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

    public string ? PaymentIntentId { get; set; } = string.Empty; 
}
