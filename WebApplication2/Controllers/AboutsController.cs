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
    public class AboutsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AboutsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Abouts
        public async Task<IActionResult> Index()
        {
              return _context.Abouts != null ? 
                          View(await _context.Abouts.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Abouts'  is null.");
        }

        // GET: Abouts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Aboutid == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aboutid,Title,Content,Imagepath,ImageFile")] About about)
        {
            if (ModelState.IsValid)
            {
                if (about.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;



                    string fileName = Guid.NewGuid().ToString() + about.ImageFile.FileName;



                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.ImageFile.CopyToAsync(fileStream);
                    }



                    about.Imagepath = fileName;
                }
                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Abouts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Aboutid,Title,Content,Imagepath,ImageFile")] About about)
        {
            if (id != about.Aboutid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {  
                try
                { 
                     if (about.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;



                    string fileName = Guid.NewGuid().ToString() + about.ImageFile.FileName;



                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.ImageFile.CopyToAsync(fileStream);
                    }



                    about.Imagepath = fileName;
                }
                _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Aboutid))
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
            return View(about);
        }

        // GET: Abouts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Aboutid == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Abouts == null)
            {
                return Problem("Entity set 'ModelContext.Abouts'  is null.");
            }
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                _context.Abouts.Remove(about);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(decimal id)
        {
          return (_context.Abouts?.Any(e => e.Aboutid == id)).GetValueOrDefault();
        }
    }
}
