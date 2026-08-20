

using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared.Response;

namespace ECommerce.ApplicaionLayer.Controllers;

[ApiController]
[Route("api/Basket")]
public class BasketController(IserviceManger _serviceManger) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerBasketResponse>> GetBasketAsync(string id)
    {
        return Ok( await _serviceManger.BasketService.GetBasketById(id));   
    }

    //delete
    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteBasketasync (string id)
    {
        bool result = await _serviceManger.BasketService.DeleteBasket(id);

        return result ? NoContent() : BadRequest();
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasketResponse>> Createorupdate(CustomerBasketResponse basket)
    {
        var result = await _serviceManger.BasketService.CreatOrupdate(basket);
        return Ok(result);
    }

}
