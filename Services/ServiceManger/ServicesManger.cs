using AutoMapper;
using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Authorizationservice;
using Services.BasketServices;
using Services.Payment;
using Services.Products;
using ServicesAbstraction.Contracts;


namespace Services.ServiceManger;

public class ServicesManger(IUnitOfWork _unitOfWork , 
    IMapper _mapper,IBasketRepository _repo,
    UserManager<ApplicationUser> _user
    ,IConfiguration _config ,IBasketRepository _basketRepository) : IserviceManger
{
    private readonly Lazy<IProductServices> _ProductService = new Lazy<IProductServices>(() => new ProductsServices(_unitOfWork, _mapper));
    private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>( ()=> new BasketService(_repo,_mapper));
    private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_user, _config));
    private readonly  Lazy<IOrderService> _orderservice = new Lazy<IOrderService>(()=> new OrderService(_mapper , _basketRepository ,_unitOfWork));
    private readonly Lazy<IPaymentService>_paymentService=new Lazy<IPaymentService>(()=>new PaymentService(_config, _basketRepository, _unitOfWork,_mapper));
    public IProductServices ProductServices => _ProductService.Value;

    public IBasketService BasketService => _basketService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;

    public IOrderService OrderService => _orderservice.Value;

    public IPaymentService paymentService =>_paymentService.Value;
}
