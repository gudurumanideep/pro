using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Checkout actioin method

        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Thankyou));

            }
            return View(Thankyou);
        }

        public IActionResult Thankyou()
        {
            return View();
        }
    }
}
