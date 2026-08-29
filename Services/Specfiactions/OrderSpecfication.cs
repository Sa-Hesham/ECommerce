using ECommerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfiactions;

public class OrderSpecfication :BaseSpecfications<Order,Guid>
{
    public OrderSpecfication(Guid id) : base(o=>o.Id==id)
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.OrderItems);


    }

    public OrderSpecfication(string userEmail):base( o=>o.UserEmail==userEmail) 
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.OrderItems);

    }
}
