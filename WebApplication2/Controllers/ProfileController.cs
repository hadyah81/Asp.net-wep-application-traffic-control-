using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Razorpay.Api;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ModelContext _context;
       private readonly IWebHostEnvironment webHostEnvironment;
        public ProfileController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;

        }
        public IActionResult getcustomerviolation()
        {
            var cust=_context.Customers.FirstOrDefault(x=>x.Id== HttpContext.Session.GetInt32("Id"));
            var veh=_context.Vehicles.Where(y=>y.CustomerId== HttpContext.Session.GetInt32("Id")).ToList();
            var io = _context.Violations.Where(y => y.CustomerId == HttpContext.Session.GetInt32("Id")).ToList();
            /*    var ids=veh.Select(r=>r.Vehicleid).ToList();
                var vio=_context.Violations.Where(e=>ids.Contains(e.Violationid)).ToList();*/
            var view3 = new join2
            {
                Customer=cust,
                Vehicles=veh,
                Violations=io,
            };
              return View(view3);



            
        }


    public IActionResult VehicleViolations(int vehicleId)
        {
            /*  var vehicle = _context.Vehicles
                  .Include(v => v.Customer) // Include the customer information if needed
                  .Include(v => v.Violations)
                  .FirstOrDefault(v => v.Vehicleid == vehicleId);

              if (vehicle == null)
              {
                  return NotFound(); // Handle the case where the vehicle is not found
              }
            */
            var vehicle = _context.Violations.ToList();

            return View(vehicle);
        }

        
        public IActionResult Create()
        {
            ViewData["CustomerId"] = HttpContext.Session.GetInt32("Id");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vehicleid,StructureNo,VehicleRegistrationNumber,RegistrationStatus,VehicleCategory,VehicleType,VehicleNationality,Model,Color,Year,CustomerId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = HttpContext.Session.GetInt32("Id");
            return View(vehicle);
        }

        public IActionResult accept()
        {
            /*   ViewBag.Name = HttpContext.Session.GetString("AdminName");*/

            ViewBag.fname = HttpContext.Session.GetString("fname");
            ViewBag.LNAME = HttpContext.Session.GetString("Lname");
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.pass = HttpContext.Session.GetString("Password");
            ViewBag.contact = HttpContext.Session.GetString("ContactNumber");
            ViewBag.id = HttpContext.Session.GetInt32("Id");
            return View();
        }
        public IActionResult customercarsandviolation(decimal? id)
        {
            /*  ViewBag.id = HttpContext.Session.GetInt32("Id");*/
                  var customer = _context.Customers.ToList();
            /*  var customer = */
              var violations = _context.Violations.ToList();
              var vehicle = _context.Vehicles.ToList();
              var result = from c in customer
                           join s in vehicle on c.Id equals s.CustomerId
                           join r in violations   on s.Vehicleid   equals r.CustomerId

                           select new join1 { violation = r, customer = c, vehicle = s };
            /*   var customer=_context.Customers.FirstOrDefault(x => x.Id == HttpContext.Session.GetInt32("Id"));
                 var vehicle = _context.Vehicles.Where(v => v.CustomerId == HttpContext.Session.GetInt32("Id"));
                 var vehicleid=vehicle.Select(s=>s.Vehicleid).ToList();
                 var violation=_context.Violations.Where(i=>  vehicleid.Contains(i.Vehicleid)).ToList();


                 */
            /*
            int?Id=HttpContext.Session.GetInt32("Id");
                 if (id == null)
                 {
                     return NotFound();
                 }
                 Vehicle v = new Vehicle();
                 var user=_context.Customers.Include(c=>c.Vehicles).FirstOrDefault(u=>u.Id==);*/
            
        /*        var car = _context.Vehicles.Include(c => c.Violations).FirstOrDefault(c => c.Vehicleid == id);
                return View(car);*/
            
     return View(result);
        }
        /*  var result = from c in customer
                        join u in userlogin on c.Id equals u.CustomerId
                        join r in role on u.RoleId equals r.Id
                        select new join1 { customer = c, role = r, UserLogin = u };
        */
      










        // GET: Customers/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editconfirmed(decimal id, [Bind("Id,Fname,Lname,Gender,Ssn,Nationality,Licensenumber,LicenseCategory,LicensingCenter,Contactnumber,Email,ImagePath,ImageFile")] Models.Customer customer)
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
                return RedirectToAction("Index", "User");
            }
            return View(customer);
        }
        private bool CustomerExists(decimal id)
        {
            throw new NotImplementedException();
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {

            ViewBag.id = HttpContext.Session.GetInt32("Id");
            /*  vehicle.CustomerId = user.Id;*/
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.Vehicleid == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("Id");
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'ModelContext.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(decimal id)
        {
            return (_context.Vehicles?.Any(e => e.Vehicleid == id)).GetValueOrDefault();
        }



        // GET: Violations/Details/5
        public async Task<IActionResult> Detailsviolation(decimal? id)
        {
            var user = _context.Customers.Where(x => x.Id == HttpContext.Session.GetInt32("Id")).FirstOrDefault();
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


        public IActionResult Details(decimal id)
        {
            /*   ViewBag.Name = HttpContext.Session.GetString("AdminName");*/

            ViewBag.fname = HttpContext.Session.GetString("fname");
            ViewBag.LNAME = HttpContext.Session.GetString("Lname");
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.pass = HttpContext.Session.GetString("Password");
            ViewBag.contact = HttpContext.Session.GetString("ContactNumber");
            ViewBag.id = HttpContext.Session.GetInt32("Id");
            id = ViewBag.id;
            return View();
        }























    }


}











  


