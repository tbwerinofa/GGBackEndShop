using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DomainService;
using System.Security.Claims;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
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
            var model = await _productService.GetById(productId);
           if(model.Id == 0)
                return BadRequest();

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewProductModel product)
        {
            SaveResult saveResult = await _productService.Create(product);
            return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductModel product)
        {
            SaveResult saveResult = await _productService.Update(product);
            return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            SaveResult saveResult = await _productService.Delete(productId);
            return saveResult.IsSuccess ? Ok(): BadRequest(saveResult.Message);
        }
    }
}
