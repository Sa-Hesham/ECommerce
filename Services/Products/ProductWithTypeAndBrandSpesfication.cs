using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using Services.Specfiactions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products;

internal class ProductWithTypeAndBrandSpesfication : BaseSpecfications<Product , int>
{

    public ProductWithTypeAndBrandSpesfication(ProductFiltiration filtiration) :
        base(p =>
        (!filtiration.brandId.HasValue || p.BrandId == filtiration.brandId.Value) &&
        (!filtiration.productTypeId.HasValue || p.ProductTypeId == filtiration.productTypeId.Value)&&
        (string.IsNullOrEmpty(filtiration.Search)||p.Name.ToLower().Contains(filtiration.Search.ToLower() ))) {

        AddInclude(P => P.ProductType!);
        AddInclude(p => p.Brand!);
        switch (filtiration.sort)
        {
            case ProductSortingOptions.NameAsc:
                orderby(p => p.Name);
                break;

            case ProductSortingOptions.NameDesc:
                orderByDescending(p => p.Name);
                break;

            case ProductSortingOptions.PriceAsc:
                orderby(p => p.price);
                break;

            case ProductSortingOptions.PriceDesc:
                orderByDescending(p => p.price);
                break;
        }
        if (filtiration.PageIndex.HasValue && filtiration.PageSize.HasValue)
        {
            AddPagination(filtiration.PageIndex.Value, filtiration.PageSize.Value);
        }
    }
    public ProductWithTypeAndBrandSpesfication(int ProductId) :base(P=>P.Id== ProductId)
    {
        AddInclude(P => P.ProductType!);
        AddInclude(p => p.Brand!);
    }





}
