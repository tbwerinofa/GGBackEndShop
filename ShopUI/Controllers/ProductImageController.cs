using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopUI.APIClient;
using ShopUI.Models;
using System.Reflection;

namespace ShopUI.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly IProductAPIClient _productService;

        public ProductImageController(IProductAPIClient productService)
        {
            _productService = productService;
        }

        #region Read
        public async Task<ActionResult> Index(int productId)
        {
            var model = await _productService.GetModelList("");
            return View(model.Where(a=>a.Id == productId));
        }
        #endregion
        // GET: ProductController/Edit/5
        public async Task<ActionResult> Create(int productId)
        {
            var parent = await _productService.GetModelById(productId);

            var model = new ProductImageModel
            {
                ProductId = productId,
            };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFiles(int productId)
        {
            long size = 0;
            var files = Request.Form.Files;
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true,
                Message = $"{files.Count} file(s) / {size} bytes uploaded successfully!"
            };

            try
            {

                if (files[0].Length <= 500000)
                {
                    var model = new ProductImageModel
                    {
                        FormFile = files[0],
                        ProductId = productId

                    };
                    saveResult = await _productService.UploadProductImage(model);
                }
                else
                {
                    saveResult.Message = $"File must be less than 500kb!";
                    saveResult.IsSuccess = false;
                }

            }
            catch (Exception exception)
            {
                return Json(new
                {
                    success = false,
                    response = exception.Message
                });
            }

            return Json(new
            {
                success = true,
                response = "File uploaded."
            });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var isSuccess = await _productService.DeleteById(id);
            return RedirectToAction("Index");
        }


    }
}
