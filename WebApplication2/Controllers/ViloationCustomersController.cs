using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ViloationCustomersController : Controller
    {
        private readonly ModelContext _context;

        public ViloationCustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: ViloationCustomers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ViloationCustomers.Include(v => v.Customer).Include(v => v.Violation);
            return View(await modelContext.ToListAsync());
        }

        // GET: ViloationCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ViloationCustomers == null)
            {
                return NotFound();
            }

            var viloationCustomer = await _context.ViloationCustomers
                .Include(v => v.Customer)
                .Include(v => v.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viloationCustomer == null)
            {
                return NotFound();
            }

            return View(viloationCustomer);
        }

        // GET: ViloationCustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["Violationid"] = new SelectList(_context.Violations, "Violationid", "Violationid");
            return View();
        }

        // POST: ViloationCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateFrom,DateTo,CustomerId,Violationid")] ViloationCustomer viloationCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viloationCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", viloationCustomer.CustomerId);
            ViewData["Violationid"] = new SelectList(_context.Violations, "Violationid", "Violationid", viloationCustomer.Violationid);
            return View(viloationCustomer);
        }

        // GET: ViloationCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ViloationCustomers == null)
            {
                return NotFound();
            }

            var viloationCustomer = await _context.ViloationCustomers.FindAsync(id);
            if (viloationCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", viloationCustomer.CustomerId);
            ViewData["Violationid"] = new SelectList(_context.Violations, "Violationid", "Violationid", viloationCustomer.Violationid);
            return View(viloationCustomer);
        }

        // POST: ViloationCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,DateFrom,DateTo,CustomerId,Violationid")] ViloationCustomer viloationCustomer)
        {
            if (id != viloationCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viloationCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViloationCustomerExists(viloationCustomer.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", viloationCustomer.CustomerId);
            ViewData["Violationid"] = new SelectList(_context.Violations, "Violationid", "Violationid", viloationCustomer.Violationid);
            return View(viloationCustomer);
        }

        // GET: ViloationCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ViloationCustomers == null)
            {
                return NotFound();
            }

            var viloationCustomer = await _context.ViloationCustomers
                .Include(v => v.Customer)
                .Include(v => v.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viloationCustomer == null)
            {
                return NotFound();
            }

            return View(viloationCustomer);
        }

        // POST: ViloationCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ViloationCustomers == null)
            {
                return Problem("Entity set 'ModelContext.ViloationCustomers'  is null.");
            }
            var viloationCustomer = await _context.ViloationCustomers.FindAsync(id);
            if (viloationCustomer != null)
            {
                _context.ViloationCustomers.Remove(viloationCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViloationCustomerExists(decimal id)
        {
          return (_context.ViloationCustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
