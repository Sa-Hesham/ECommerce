using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Authorizationservice;
using Services.BasketServices;
using Services.Products;
using ServicesAbstraction.Contracts;


namespace Services.ServiceManger;

public class ServicesManger(IUnitOfWork _unitOfWork , 
    IMapper _mapper,IBasketRepository _repo,
    UserManager<ApplicationUser> _user
    ,IConfiguration _config) : IserviceManger
{
    private readonly Lazy<IProductServices> _ProductService = new Lazy<IProductServices>(() => new ProductsServices(_unitOfWork, _mapper));
    private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>( ()=> new BasketService(_repo,_mapper));
    private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_user, _config));

    public IProductServices ProductServices => _ProductService.Value;

    public IBasketService BasketService => _basketService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
