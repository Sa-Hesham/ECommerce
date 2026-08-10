using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public class Product :BaseEntity <int>
{
    public string Name { get;  set; } = null!;

    public string Description { get;  set; } = null!;
    public string PictureUrl { get;  set; } = null!;

    public decimal price { get;  set; }


    public int BrandId { get;  set; }    

   public ProductBrand ? Brand { get;  set; }  



    public int ProductTypeId { get;  set; }


    public ProductType ? ProductType { get;  set; }        


}
