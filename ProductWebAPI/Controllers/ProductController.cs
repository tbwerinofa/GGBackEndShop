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
    public class ProductController : ControllerBase
    {
        #region global fields

        private readonly IProductService _productService;
        #endregion

        #region CTOR
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Action Results

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }            
            var model = await _productService.GetModelList(userId);
            return Ok(model);
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<ProductModel>> GetById(int productId)
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var model = await _productService.GetById(productId,userId);
           if(model.Id == 0)
                return BadRequest();

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewProductModel product)
        {
            if(ModelState.IsValid) {

                string? userId = GetUserId();

                if (userId == null)return Unauthorized();

                product.UserId = userId;

                SaveResult saveResult = await _productService.Create(product);
                return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);

            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductModel product)
        {
            if (ModelState.IsValid)
            {

                string? userId = GetUserId();

                if (userId == null) return Unauthorized();

                product.UserId = userId;
                SaveResult saveResult = await _productService.Update(product);
               return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);

            }
            return BadRequest();
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            string? userId = GetUserId();

            if (userId == null) return Unauthorized();
            SaveResult saveResult = await _productService.Delete(productId, userId);
            return saveResult.IsSuccess ? Ok(): BadRequest(saveResult.Message);
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
