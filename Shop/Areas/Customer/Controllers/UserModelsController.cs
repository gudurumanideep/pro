using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/UserModels
        public async Task<IActionResult> Index()
        {
              return _context.UserModels != null ? 
                          View(await _context.UserModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UserModels'  is null.");
        }

        // GET: Customer/UserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels
                .FirstOrDefaultAsync(m => m.id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: Customer/UserModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,uname,email,password,mobile")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: Customer/UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Customer/UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,uname,email,password,mobile")] UserModel userModel)
        {
            if (id != userModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: Customer/UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels
                .FirstOrDefaultAsync(m => m.id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: Customer/UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserModels'  is null.");
            }
            var userModel = await _context.UserModels.FindAsync(id);
            if (userModel != null)
            {
                _context.UserModels.Remove(userModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
          return (_context.UserModels?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
