using LabAssignment2.Models;
using LabAssignment2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabAssignment2.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var products = _productRepository.ListProducts();
            return View(products);
        }
                
        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _productRepository.ListCategories().ToList();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            try
            {
                ModelState.Remove("ProductId");
                if (ModelState.IsValid)
                {
                    _productRepository.AddProduct(model);
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _productRepository.ListCategories().ToList();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _productRepository.ListCategories().ToList();
            var product = _productRepository.GetProduct(id);
            return View("Create", product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.UpdateProduct(model);
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _productRepository.ListCategories().ToList();
                return View("Create", model);
            }
            catch
            {
                return View("Create", model);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product model = _productRepository.GetProduct(id);
            if (model != null)
            {
                _productRepository.DeleteProduct(id);
            }
            return RedirectToAction("Index");
        }
    }
}
