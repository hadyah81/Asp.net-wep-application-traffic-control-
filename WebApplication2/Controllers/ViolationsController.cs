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
    public class ViolationsController : Controller
    {
        private readonly ModelContext _context;

        public ViolationsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Violations
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Violations.Include(v => v.Customer).Include(v => v.Vehicle);
            return View(await modelContext.ToListAsync());
        }

        // GET: Violations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.Customer)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.Violationid == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // GET: Violations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["Vehicleid"] = new SelectList(_context.Vehicles, "Vehicleid", "Vehicleid");
            return View();
        }

        // POST: Violations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Violationid,CustomerId,Vehicleid,PoliceDirectorate,CourtName,ViolationDate,ViolationTime,ViolationLocation,Street,ViolationDescription,Fineamount,Citationnumber")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", violation.CustomerId);
            ViewData["Vehicleid"] = new SelectList(_context.Vehicles, "Vehicleid", "Vehicleid", violation.Vehicleid);
            return View(violation);
        }

        // GET: Violations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations.FindAsync(id);
            if (violation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", violation.CustomerId);
            ViewData["Vehicleid"] = new SelectList(_context.Vehicles, "Vehicleid", "Vehicleid", violation.Vehicleid);
            return View(violation);
        }

        // POST: Violations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Violationid,CustomerId,Vehicleid,PoliceDirectorate,CourtName,ViolationDate,ViolationTime,ViolationLocation,Street,ViolationDescription,Fineamount,Citationnumber")] Violation violation)
        {
            if (id != violation.Violationid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationExists(violation.Violationid))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", violation.CustomerId);
            ViewData["Vehicleid"] = new SelectList(_context.Vehicles, "Vehicleid", "Vehicleid", violation.Vehicleid);
            return View(violation);
        }

        // GET: Violations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Violations == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.Customer)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.Violationid == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // POST: Violations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Violations == null)
            {
                return Problem("Entity set 'ModelContext.Violations'  is null.");
            }
            var violation = await _context.Violations.FindAsync(id);
            if (violation != null)
            {
                _context.Violations.Remove(violation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationExists(decimal id)
        {
          return (_context.Violations?.Any(e => e.Violationid == id)).GetValueOrDefault();
        }
    }
}
