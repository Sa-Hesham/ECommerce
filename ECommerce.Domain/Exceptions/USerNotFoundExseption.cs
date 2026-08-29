using ECommerce.Domain.Entities;


namespace ECommerce.Domain.Exceptions;

public class USerNotFoundExseption (string email) : NotFoundException( $"user with email {email} is not found ")
{
}
