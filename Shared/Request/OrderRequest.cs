using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Request;

public record OrderRequest
{
    public string BasketId {  get; init; } = string.Empty;  

    public AddressDto Address { get; init; } = null!;

    public int DeliveryMethodId { get; init; }
}
