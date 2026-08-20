using AutoMapper;
using ECommerce.Domain.Abstraction;
using Services.BasketServices;
using Services.Products;
using ServicesAbstraction.Contracts;


namespace Services.ServiceManger;

public class ServicesManger(IUnitOfWork _unitOfWork , IMapper _mapper,IBasketRepository _repo) : IserviceManger
{
    private readonly Lazy<IProductServices> _ProductService = new Lazy<IProductServices>(() => new ProductsServices(_unitOfWork, _mapper));
    private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>( ()=> new BasketService(_repo,_mapper));


    public IProductServices ProductServices => _ProductService.Value;

    public IBasketService BasketService => _basketService.Value;
}
