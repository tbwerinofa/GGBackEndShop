using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DocumentService;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;

        public ProductImageController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }
        [HttpPost]
        public ActionResult Create([FromForm] ProductImageModel productImage)
        {

           bool success =_documentManager.UpLoad(productImage);
            if(success)
            {

            }
            return Ok(true);
        }
    }
}

