using ECommerce.Domain.Entities;


namespace ECommerce.Domain.Exceptions;

public class OrderNotFoundException(Guid id) : NotFoundException($"Order with Id :{id} not Found ") { 




};
