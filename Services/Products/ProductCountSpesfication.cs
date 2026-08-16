using ECommerce.Domain.Entities;
using Services.Specfiactions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products;

public class ProductCountSpesfication : BaseSpecfications<Product, int>
{
    public ProductCountSpesfication(ProductFiltiration filtiration) : base(p =>
        (!filtiration.brandId.HasValue || p.BrandId == filtiration.brandId.Value) &&
        (!filtiration.productTypeId.HasValue || p.ProductTypeId == filtiration.productTypeId.Value) &&
        (string.IsNullOrEmpty(filtiration.Search) || p.Name.ToLower().Contains(filtiration.Search.ToLower())))
    { 
    
    
    
    
    
    
    }


}
   
