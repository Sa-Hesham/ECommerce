using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction.Contracts;

public interface IserviceManger
{

    public IProductServices ProductServices { get; }    

    public IBasketService BasketService { get; }    
}
