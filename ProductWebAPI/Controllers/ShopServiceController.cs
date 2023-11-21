using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DomainService;
using static ProductWebAPI.Utilities;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopServiceController : ControllerBase
    {
        #region global fields
        private readonly IShopService _shopServiceService;
        #endregion

        #region constructors
        public ShopServiceController(IShopService shopService)
        {
            _shopServiceService = shopService;
        }
        #endregion

        #region Action Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetShopping()
        {

            string? userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var model = await _shopServiceService.GetModelList(userId);
            return Ok(model);
        }

        #endregion

        #region private methods
        private string? GetUserId()
        {
            if (User.Claims == null)
            {
                return null;
            }

            return User.Claims.ExtractUserId(ClaimTypeEnum.sid.ToString());
        }
        #endregion
    }
}

