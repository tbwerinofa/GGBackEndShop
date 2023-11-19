using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopUI.APIClient;
using ShopUI.Models;
using System.Reflection;

namespace ShopUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAPIClient _productService;

        public ProductController(IProductAPIClient productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _productService.GetModelList("");
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductModel model)
        {


            if (ModelState.IsValid)
            {
                var isSuccess = await _productService.Create(model);
                if (isSuccess)
                {

                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, "Failed to Save");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _productService.GetModelById(id);
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _productService.Update(model);
                if (isSuccess)
                {

                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, "Failed to Register");
            }
            return View(model);

        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var isSuccess = await _productService.DeleteById(id);
            return RedirectToAction("Index");
        }


    }
}
