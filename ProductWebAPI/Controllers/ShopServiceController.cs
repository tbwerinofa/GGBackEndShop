using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DomainService;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopServiceController : ControllerBase
    {

        private readonly IShopService _shopServiceService;
        public ShopServiceController(IShopService shopService)
        {
            _shopServiceService = shopService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetShopping()
        {
            var model = await _shopServiceService.GetModelList();
            return Ok(model);
        }
    }
}

