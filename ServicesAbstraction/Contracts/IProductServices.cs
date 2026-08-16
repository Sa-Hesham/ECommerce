using Shared;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction.Contracts;

public interface IProductServices
{
    public Task<PaginateResult<ProductResponse>> GetProductsAsync(ProductFiltiration filtiration, CancellationToken ct = default);


    public Task<ProductResponse?> GetProduct ( int PrductId , CancellationToken ct = default )  ;




    public Task<IEnumerable<productTypeResponse>> GetAllProductsType ( CancellationToken ct = default );



    public Task<IEnumerable< BrandResponse>> GetBrands( CancellationToken ct = default )  ;


    public Task ToggelSatus(int id);



   




}
