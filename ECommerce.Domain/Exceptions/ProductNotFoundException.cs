using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException( int id ) :base($"Product with {id} is not Found")
    {
        
    }
}
