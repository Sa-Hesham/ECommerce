using Shared.Request;
using Shared.Response;


namespace ServicesAbstraction.Contracts;

public interface IOrderService
{
    // getOrderById(Guid) => orderResult

    Task<OrderResponse> GetOrderByIdAsync(Guid id,CancellationToken ct =default);


    // ordesrbyEmail(Email) => Ienumerable<orderResult>
    Task<IEnumerable< OrderResponse>> GetOrderByEmailAsync(string userEmail, CancellationToken ct = default);

    //CreateOrder (orderRequest + email ) => orderresult
    Task<OrderResponse> CreateOrderAsync(OrderRequest request, string userEmail, CancellationToken ct = default);
    //GetDeliveryMethod => ienumerable<methodResult>
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync(CancellationToken ct = default);
}
