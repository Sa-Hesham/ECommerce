

namespace ECommerce.Domain.Entities;

public class DelivermethodNotFoundException(int id ) : NotFoundException($"Delevery Method not Found  with {id}")
{
}
