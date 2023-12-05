using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger,ModelContext context)
        {
            _logger = logger;
            this._context = context;    
        }

        public IActionResult Index()
        {
         
            var h = _context.Homes.ToList();
          

            return View(h);
            
        }

        public IActionResult AboutUs()
        {
            var b = _context.Abouts.ToList();
            return View(b);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Testmonial()
        {
           var m = _context.Testimonials.Where(x => x.Status == "accept").ToList();
     
            return View(m);
        }
        public IActionResult profile()
        {
            return View();
        }
        public IActionResult SendMail(EmailModel model)
        {
            using (MailMessage mm = new MailMessage(model.Email, model.TO))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;
                if (string.IsNullOrWhiteSpace(model.TO))
                {
                    // Handle the case where the 'to' address is missing or empty
                    ModelState.AddModelError("To", "The 'to' email address is required.");
                    return View(model);
                }
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    // Handle the case where the 'from' address is missing or empty
                    ModelState.AddModelError("Email", "The 'from' email address is required.");
                    return View(model);
                }
                using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential cred = new NetworkCredential(model.Email, model.password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = cred;
                smtp.Port = 587;
                smtp.Send(mm);
                ViewBag.message = "Email sent";
            }

        }
           
                return View(model);
        }

       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}