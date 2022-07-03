using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Area("Admin")]
    public class AdminModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminModels
        public async Task<IActionResult> Index()
        {
            return _context.AdminModel != null ?
                        View(await _context.AdminModel.ToListAsync()) :
                        Problem("Entity set 'Shop_DBContext.AdminModel'  is null.");
        }

        // GET: AdminModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminModel == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminModel == null)
            {
                return NotFound();
            }

            return View(adminModel);
        }

        // GET: AdminModels/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login(string Login, string uname, string pwd, string mode)
        {

            string cs = @"server=LAPTOP-2T2VAUO8\SQLEXPRESS;integrated security=true;database=LoginDB";
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();
            if (mode == "Admin")
            {
                string query = "select uname,password from tblAdmin";
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string un = dr[0].ToString();
                    string pn = dr[1].ToString();

                    if (uname.Equals(un) && pwd.Equals(pn))
                    {
                        return Redirect("/Admin/Products");
                    }
                    else
                    {
                        ViewData["message"] = "invalid credits";

                    }
                }

            }
            if (mode == "User")
            {
                string query = "select uname,password from UserModel";
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string un = dr[0].ToString();
                    string pw = dr[1].ToString();
                    if (uname.Equals(un) && pwd.Equals(pw))
                    {
                        return Redirect("/Customer/Home/Index");
                    }
                    else
                    {
                        ViewData["message"] = "Invalid Credentials....";
                    }


                }



            }

            return View();
        }

        // POST: AdminModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,uname,password")] AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminModel);
        }

        // GET: AdminModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdminModel == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModel.FindAsync(id);
            if (adminModel == null)
            {
                return NotFound();
            }
            return View(adminModel);
        }

        // POST: AdminModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,uname,password")] AdminModel adminModel)
        {
            if (id != adminModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminModelExists(adminModel.Id))
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
            return View(adminModel);
        }

        // GET: AdminModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdminModel == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminModel == null)
            {
                return NotFound();
            }

            return View(adminModel);
        }

        // POST: AdminModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminModel == null)
            {
                return Problem("Entity set 'Shop_DBContext.AdminModel'  is null.");
            }
            var adminModel = await _context.AdminModel.FindAsync(id);
            if (adminModel != null)
            {
                _context.AdminModel.Remove(adminModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminModelExists(int id)
        {
            return (_context.AdminModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
