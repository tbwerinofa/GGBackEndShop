using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DataRepository.Service;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var model = await _productService.GetModelList();
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
