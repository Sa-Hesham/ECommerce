
using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using ServicesAbstraction.Contracts;
using Shared;
using Shared.Response;

namespace Services.Products;

public class ProductsServices(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductServices
{
    // Ineed ?UnitofWork 

  
    public async Task<IEnumerable<productTypeResponse>> GetAllProductsType(CancellationToken ct = default)
    {
        var Entity = await _unitOfWork.GetRepo<ProductType, int>().Getallasync(ct);
       var ProductType =  _mapper.Map<IEnumerable<productTypeResponse>>(Entity);
        return ProductType;
    }
    public async Task<IEnumerable<BrandResponse>> GetBrands(CancellationToken ct = default)
    {
        var Entity = await _unitOfWork.GetRepo<ProductBrand, int>().Getallasync(ct);
        var Broductbrand = _mapper.Map<IEnumerable<BrandResponse>>(Entity);
        return Broductbrand;    
    }

    public async Task<ProductResponse?> GetProduct(int PrductId, CancellationToken ct = default)
    {
        var specfications = new ProductWithTypeAndBrandSpesfication(PrductId);
        var Entity = await _unitOfWork.GetRepo<Product, int>().GettByIdAsync(specfications, ct);
        var product = _mapper.Map<ProductResponse>(Entity);
        return product;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(ProductFiltiration filtiration, CancellationToken ct = default)
    {
        var spacefication = new ProductWithTypeAndBrandSpesfication( filtiration);
        var Entity = await _unitOfWork.GetRepo<Product, int>().Getallasync(spacefication, ct);
        var products = _mapper.Map<IEnumerable< ProductResponse>>(Entity);
        return products;
    }

    public Task ToggelSatus(int id)
    {
        throw new NotImplementedException();
    }
}
