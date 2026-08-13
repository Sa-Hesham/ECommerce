
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;

namespace ECommerce.ApplicaionLayer.Controllers;


[ApiController]
[Route("Api/Products")]
public class ProductController(IserviceManger _serviceManger) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken ct)
    {
        var rsult = await _serviceManger.ProductServices.GetProductsAsync(ct);
        return Ok(rsult);
    }


    [HttpGet("{Id:int}")]
    public  async Task<IActionResult> GetProduct(int Id, CancellationToken ct) { 

        var product =   await _serviceManger.ProductServices.GetProduct(Id, ct);

        if (product is null)
            return NotFound();


        return Ok(product);

    
    
    }


    [HttpGet("Brands")]
    public async Task<IActionResult>GetBrands(CancellationToken ct)
    {

        return Ok(await _serviceManger.ProductServices.GetBrands(ct));
    }


    [HttpGet("Types")]
    public async Task <IActionResult> ProductsType (CancellationToken ct)
    {

        return Ok(await _serviceManger.ProductServices.GetAllProductsType(ct));
    }
}
