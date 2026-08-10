using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public  class ProductType :BaseEntity 
{
    public string Name { get; private set; } = null!;


    public ICollection<Product> products { get; private set; }  =new List<Product>();   
}
