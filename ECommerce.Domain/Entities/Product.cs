using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public class Product :BaseEntity
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;
    public string PictureUrl { get; private set; } = null!;

    public decimal price { get; private set; }


    public Guid BrandId { get; private set; }    

   public ProductBrand ? Brand { get; private set; }  



    public Guid ProductTypeId { get; private set; }


    public ProductType ? ProductType { get; private set; }        


}
