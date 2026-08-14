using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using Services.Specfiactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products;

internal class ProductWithTypeAndBrandSpesfication : BaseSpecfications<Product , int>
{

    public ProductWithTypeAndBrandSpesfication(int? brandId, int? productTypeId) :
        base(p =>
        (!brandId.HasValue || p.BrandId == brandId.Value) &&
        (!productTypeId.HasValue || p.ProductTypeId == productTypeId.Value)) {

        AddInclude(P => P.ProductType!);
        AddInclude(p => p.Brand!);

    
    }
    public ProductWithTypeAndBrandSpesfication(int ProductId) :base(P=>P.Id== ProductId)
    {
        AddInclude(P => P.ProductType!);
        AddInclude(p => p.Brand!);
    }





}
