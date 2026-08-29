using Shared.Response;

namespace ServicesAbstraction.Contracts;

public interface IPaymentService
{

    public Task<CustomerBasketResponse> GetorupdatePaymentIntentAsync(string basketId);

}
