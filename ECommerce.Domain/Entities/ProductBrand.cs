using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public class ProductBrand :BaseEntity<int>
{

    public string Name { get;  set; } = null!;

    public ICollection<Product> products { get;  set; } = new List<Product>();  
}
