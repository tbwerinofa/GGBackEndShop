using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DocumentService;
using ProductWebAPI.Services.DomainService;
using static ProductWebAPI.Utilities;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductImageController : ControllerBase
    {
        #region global fields
        private readonly IDocumentManager _documentManager;
        private readonly IProductImageService _productImageService;
        #endregion

        #region CTOR
        public ProductImageController(IDocumentManager documentManager, IProductImageService productImageService)
        {
            _documentManager = documentManager;
            _productImageService = productImageService;
        }

        #endregion

        #region Action Results
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
            string? userId = GetUserId();

            if (userId == null) return Unauthorized();

            var model = await _productImageService.GetById(productImageId, userId);

            if(!String.IsNullOrEmpty(model.FileNameGuid) && await _documentManager.Delete(model.FileNameGuid)) { 
                saveResult =await _productImageService.Delete(productImageId);
            }
           return saveResult.IsSuccess ? Ok() : BadRequest(saveResult.Message);
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

