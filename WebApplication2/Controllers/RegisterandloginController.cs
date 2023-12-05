using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RegisterandloginController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterandloginController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind(("Id,Fname,Lname,Gender,Ssn,Nationality,Licensenumber,LicenseCategory,LicensingCenter,Contactnumber,Email,ImagePath,ImageFile"))] Customer customer, string UserName, string Pass)
        {
            if (ModelState.IsValid)
            {
                if (customer.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;



                    string fileName = Guid.NewGuid().ToString() + customer.ImageFile.FileName;



                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await customer.ImageFile.CopyToAsync(fileStream);
                    }



                    customer.ImagePath = fileName;
                }
                var user = _context.UserLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (user == null)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    UserLogin userlogin = new Models.UserLogin();
                    userlogin.RoleId = 2;
                    userlogin.UserName = UserName;
                    userlogin.Password = Pass;
                    userlogin.CustomerId = customer.Id;

                    _context.Add(userlogin);
                    await _context.SaveChangesAsync();



                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.Error = "the user name already exist please try another ";
                }
            }
            return View(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Login([Bind("Id,UserName,Password")] UserLogin userlogin)
        {
            var auth = _context.UserLogins.Where(x => x.UserName == userlogin.UserName && x.Password == userlogin.Password).FirstOrDefault();
           
            if (auth != null)
            {
                var user = _context.Customers.Where(x => x.Id == auth.CustomerId).FirstOrDefault();
                switch (auth.RoleId)
                {
                    case 1:
                        
                        
                       HttpContext.Session.SetString("fname",user.Fname);
                        HttpContext.Session.SetString("Lname",user.Lname);
                        HttpContext.Session.SetString("Username",auth.UserName);
                        HttpContext.Session.SetString("Password",auth.Password);
                            HttpContext.Session.SetString("ContactNumber",user.Contactnumber);
                        HttpContext.Session.SetInt32("Id", (int)auth.CustomerId);
                      
                        return RedirectToAction("Admin", "Admin");

                    case 2:
                        HttpContext.Session.SetInt32("Id", (int)auth.CustomerId);
                        HttpContext.Session.SetString("fname", user.Fname);
                        HttpContext.Session.SetString("Lname", user.Lname);
                        HttpContext.Session.SetString("Username", auth.UserName);
                        HttpContext.Session.SetString("Password", auth.Password);

                        return RedirectToAction("Index", "User");

                }
            }

            return View();
        }






    }
}










  