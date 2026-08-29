using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared.Request;
using Shared.Response;
using System.Security.Claims;

namespace ECommerce.ApplicaionLayer.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController(IserviceManger _servicemanger) :ControllerBase
{
    // create order 
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest ,CancellationToken ct = default)
    {
        var userEmail = User.FindFirstValue( ClaimTypes.Email)  ;
        var Orders = await _servicemanger.OrderService.CreateOrderAsync(orderRequest,userEmail!,ct);
        return Ok(Orders);
    }

    // getOrderByID 

    [HttpGet("{id:Guid}")]

    public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id, CancellationToken ct = default)
    {

        var order = await _servicemanger.OrderService.GetOrderByIdAsync(id, ct);
        return Ok(order);

    }


    // GetOrdersByEmail 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> getOrdersByEmail(CancellationToken ct = default)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email)   ;
        var orders = await _servicemanger.OrderService.GetOrderByEmailAsync(userEmail!, ct);
        return Ok(orders);

    }



    [HttpGet("deliveryMethod")]
    public async Task<ActionResult<DeliveryMethodResponse>> GetDeliverMethods(CancellationToken ct = default) { 
    
        var delivery = await _servicemanger.OrderService.GetDeliveryMethodsAsync(ct);

        return Ok(delivery);
    
    
    }

}
