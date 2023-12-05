using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
		private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public AdminController(ModelContext context)
        {
            _context = context;
        }
		public IActionResult Admin()
        {
            ViewBag.usercount = _context.Customers.Count();
            ViewBag.car = _context.Vehicles.Count();
            return View();
        }
       
        public IActionResult Index()
        {
            ViewBag.fname = HttpContext.Session.GetString("fname");
            ViewBag.LNAME = HttpContext.Session.GetString("Lname");
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.pass = HttpContext.Session.GetString("Password");
            ViewBag.id = HttpContext.Session.GetInt32("Id");
            var test = _context.Testimonials.ToList();
         
            return View(test);
        }
        public IActionResult Index1()
        {
            ViewBag.fname = HttpContext.Session.GetString("fname");
            ViewBag.LNAME = HttpContext.Session.GetString("Lname");
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.pass = HttpContext.Session.GetString("Password");
            ViewBag.email = HttpContext.Session.GetString("Email");
            ViewBag.id = HttpContext.Session.GetInt32("Id");



            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'ModelContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            testimonial.Status = "reject";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AcceptTestimonial(decimal id)
      {

            var testimonial =await  _context.Testimonials.FindAsync(id);
            testimonial.Status = "accept";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TestimonialExists(decimal id)
        {
            return (_context.Testimonials?.Any(e => e.Testimonialid == id)).GetValueOrDefault();
        }
        // GET: Testimonials/Delete/5
        public async Task<IActionResult> DeleteConfirmed(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }
        public IActionResult accept()
        {
           
           ViewBag.fname = HttpContext.Session.GetString("fname");
            ViewBag.LNAME = HttpContext.Session.GetString("Lname");
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.pass = HttpContext.Session.GetString("Password");
            ViewBag.contact = HttpContext.Session.GetString("ContactNumber");
            ViewBag.id = HttpContext.Session.GetInt32("Id");
            return View();
        }
        public async Task<IActionResult> Editconfirmed(decimal? id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("Id");


            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editconfirmed(decimal id, [Bind("Id,Fname,Lname,Gender,Ssn,Nationality,Licensenumber,LicenseCategory,LicensingCenter,Contactnumber,Email,ImagePath,ImageFile")] Customer customer)
        {
            ViewBag.id = HttpContext.Session.GetInt32("Id");

            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (customer.ImageFile != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;



                        string fileName = Guid.NewGuid().ToString() + customer.ImageFile.FileName;



                        string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await customer.ImageFile.CopyToAsync(fileStream);
                        }



                        customer.ImagePath = fileName;
                    }
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Admin", "Admin");
            }
            return View(customer);
        }
        private bool CustomerExists(decimal id)
        {
            throw new NotImplementedException();
        }

        public IActionResult MontlyReport(int year, int month)
        {
            // Retrieve data for the specified month and year from the database
            var violations = _context.Violations
                .Where(v => v.ViolationDate.Year == year && v.ViolationDate.Month == month)
                .ToList();
            decimal totalViolationPrice = violations.Sum(v => (decimal)v.Fineamount);
            ViewBag.f = totalViolationPrice;
            ViewBag.count = violations.Count();
            // Perform any necessary calculations or formatting for the report

            return View(violations);
        }
        public IActionResult MontlyReport2(int month)
        {
            var violations = _context.Violations
                .Where(v => v.ViolationDate.Month == month)
                .ToList();
            decimal totalViolationPrice = violations.Sum(v => (decimal)v.Fineamount);
            ViewBag.f = totalViolationPrice;
            ViewBag.count = violations.Count();
            // Perform any necessary calculations or formatting for the report

            return View(violations);
        }
        public IActionResult annualreport(int year)
        {
            // Retrieve data for the specified year from the database
            var violations = _context.Violations
                .Where(v => v.ViolationDate.Year == year)
                .ToList();
            decimal totalViolationPrice = violations.Sum(v => (decimal)v.Fineamount);
            ViewBag.f = totalViolationPrice;
            ViewBag.count = violations.Count();
            // Perform any necessary calculations or formatting for the report

            return View(violations);
        }






        [HttpGet]
        public IActionResult Search()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(Traffic_Violation t)
        {

            var result = _context.Violations.Where(x => x.ViolationDate >= t.StartDate && x.ViolationDate <= t.EndDate).ToList();
            ViewBag.Result = result;
            return View(t);
        }

































    }
}
