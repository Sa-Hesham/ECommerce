using ECommerce.Domain.Entities.BasketModuel;
using StackExchange.Redis;


namespace ECommerce.Inferastructure.Repositories;

public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
{
    private readonly IDatabase _database = _connection.GetDatabase();
    public async Task<bool> DeleteBasketAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public async Task<CustomerBasket?> GetBasketByIdAsync(string id)
    {
       var jsondata = await _database.StringGetAsync(id);
        if(!jsondata.IsNullOrEmpty)
        {

            return JsonSerializer.Deserialize<CustomerBasket>(jsondata!);
        }

        
        return null;
    }

    public async Task<CustomerBasket?> GreateOrUpdateAsync(CustomerBasket Basket)
    {
        var jsonData = JsonSerializer.Serialize(Basket);
      var result = await _database.StringSetAsync(Basket.Id , jsonData , TimeSpan.FromDays(30));

        return result ? await GetBasketByIdAsync(Basket.Id):null;
    }
}
