using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entity;
using Inventory.Web.Data;

namespace Inventory.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Items.Include(i => i.Borrower).Include(i => i.Buyer).Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Borrower)
                .Include(i => i.Buyer)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Reportes/Create
        public IActionResult Create()
        {
            ViewData["BorrowerID"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["BuyerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Description,Price,StoredDate,BorrowedDate,SoldDate,Status,BorrowerID,BuyerId,CategoryId,Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerID"] = new SelectList(_context.Employees, "Id", "Name", item.BorrowerID);
            ViewData["BuyerId"] = new SelectList(_context.Customers, "Id", "Name", item.BuyerId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Reportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["BorrowerID"] = new SelectList(_context.Employees, "Id", "Name", item.BorrowerID);
            ViewData["BuyerId"] = new SelectList(_context.Customers, "Id", "Name", item.BuyerId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Brand,Description,Price,StoredDate,BorrowedDate,SoldDate,Status,BorrowerID,BuyerId,CategoryId,Id")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["BorrowerID"] = new SelectList(_context.Employees, "Id", "Name", item.BorrowerID);
            ViewData["BuyerId"] = new SelectList(_context.Customers, "Id", "Name", item.BuyerId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Reportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Borrower)
                .Include(i => i.Buyer)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return _context.Items.Any(e => e.Id == id);
        }
    }
}
