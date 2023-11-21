using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DocumentService;
using ProductWebAPI.Services.DomainService;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductImageController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;
        private readonly IProductImageService _productImageService;
        public ProductImageController(IDocumentManager documentManager, IProductImageService productImageService)
        {
            _documentManager = documentManager;
            _productImageService = productImageService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] NewProductImageModel model)
        {

            ProductImageModel productImage = _documentManager.UpLoad(model);
            if (productImage.ProductId >0)
            {
                var saveResult = await _productImageService.Save(productImage);
                if (saveResult.IsSuccess)
                {
                    return Ok(true);
                }

            }
            return BadRequest(false);
        }

        [HttpDelete("{productImageId:int}")]
        public async Task<ActionResult> Delete(int productImageId)
        {
            SaveResult saveResult = new SaveResult();
            var model = await _productImageService.GetById(productImageId);

            if(!String.IsNullOrEmpty(model.FileNameGuid) && await _documentManager.Delete(model.FileNameGuid)) { 
                saveResult =await _productImageService.Delete(productImageId);
            }
           return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);
        }
    }
}

