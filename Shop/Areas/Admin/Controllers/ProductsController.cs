using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        private ApplicationDbContext _db;
       
        
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
         

        }
        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }
        //create 
        public ActionResult Create()
        {
            return View();
        }

        //Create post method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(products);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        //Post Edit Action method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                _db.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(product);
        }
        //Details Action 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        //Post Details Action method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Products product)
        {
            return RedirectToAction(actionName: nameof(Index));

        }
        //Delete Action Result
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        //Post Delete Action method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Products product)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != product.Id)
                return NotFound();
            var producttype = _db.Products.Find(id);
            if (producttype == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(producttype);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(product);
        }


    }
}
