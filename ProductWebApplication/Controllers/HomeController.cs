using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Models.Repositories;
using ProductWebApplication.Models;

namespace ProductWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productsController;

        public HomeController(IProductRepository productRepository)
        {
            _productsController = productRepository;
        }

        #region Other

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            ViewData["Message"] = "Author";
            return View();
        }

        #endregion

        #region Add Product Form
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductCreateInputModel model)
        {
            var p = _productsController.Add(model);
            if (p == null)
                return View();
            return AllProducts();
        }
        #endregion

        #region Edit Product Form
        public ActionResult EditProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProduct(ProductUpdateInputModel updateInputModel)
        {
            var p = _productsController.Update(updateInputModel);
            if (p == null)
                return BadRequest("bad request");
            return AllProducts();
        }

        #endregion

        #region All Products Form

        public ActionResult AllProducts()
        {
            return View("AllProducts", _productsController.Get());
        }

        public ActionResult Edit(string id)
        {
            var p = _productsController.Get(Guid.Parse(id));
            return View("EditProduct", new ProductUpdateInputModel(p));
        }

        public ActionResult Delete(string id)
        {
            _productsController.Delete(Guid.Parse(id));
            return AllProducts();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
