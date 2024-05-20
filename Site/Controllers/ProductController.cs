using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelAccessLayer.Models;

namespace Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _services;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductServices services, IWebHostEnvironment environment)
        {
            _services = services;
            _environment = environment;
        }


        [Authorize(Roles ="ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var result = await _services.ProductList();
            return View(result);
        }

		[Authorize(Roles = "USER")]

		[HttpGet]
        public async Task<IActionResult> ProductListUser()
        {
            var result = await _services.ProductList();
            return View(result);
        }


        // GET: ProductController/Create
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            try
            {
                string? path = _environment.WebRootPath;
                var result = await _services.AddProduct(productViewModel, path);
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Edit(int id)
        {
            var productData = await _services.EditProduct(id);
            return View(productData);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            try
            {
                string? path = _environment.WebRootPath;
                var product = _services.EditProduct(productViewModel, path);
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            string? path = _environment.WebRootPath;
            var result = await _services.DeleteProduct(id, path);
            if (result)
            {
                return RedirectToAction(nameof(ProductList));
            }
            else
            {
                return BadRequest(); // Return an error response
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyProduct(int Id)
        {
            var result =await  _services.MyProduct(Id);
            return View(result);
        }
    }
}
