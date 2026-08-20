using System;

namespace ECommerce.Domain.Entities;

public class NotFoundException :Exception
{
    public NotFoundException( string message) : base(message)   
    {
        
    }
}
