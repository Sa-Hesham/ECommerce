using AutoMapper;
using ECommerce.Domain.Abstraction;
using Services.Products;
using ServicesAbstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceManger;

public class ServicesManger(IUnitOfWork _unitOfWork , IMapper _mapper) : IserviceManger
{
    private readonly Lazy<IProductServices> _ProductService = new Lazy<IProductServices>(() => new ProductsServices(_unitOfWork, _mapper));
    

    public IProductServices ProductServices => _ProductService.Value;
}
