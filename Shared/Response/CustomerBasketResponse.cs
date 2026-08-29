using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response;

public  record CustomerBasketResponse
{

    public string Id { get; init; } = null!;

    public ICollection< BasketItemsResponse > BasketItem{ get; init; } = [];


}
