using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public class ProductBrand :BaseEntity
{

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;


    public ICollection<Product> products { get; private set; } = new List<Product>();  
}
