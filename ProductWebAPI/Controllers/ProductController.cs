using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductDBContext _dbContext;

        public ProductController(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        [Authorize]
        [HttpGet("GetProducts")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var headers = HttpContext.Request.Headers;
            var model = Enumerable.Empty<Product>();
            return Ok(model);
        }
        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var product = await _dbContext.Product.FindAsync(productId);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _dbContext.Product.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _dbContext.Product.FindAsync(productId);
            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
