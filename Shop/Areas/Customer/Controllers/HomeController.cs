using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Utility;
using System.Diagnostics;

namespace Shop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Get product details action method

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

        //Post product details action method
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult ProductDetail(int? id)
        {
            List<Products>products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            products = HttpContext.Session.Get<List<Products>>("products");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (products==null)
                products=new List<Products>();
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }
        public IActionResult Cart()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            List<Products> products= HttpContext.Session.Get<List<Products>>("products");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            
                return View(products);
        }


        [ActionName("Remove")]
        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}