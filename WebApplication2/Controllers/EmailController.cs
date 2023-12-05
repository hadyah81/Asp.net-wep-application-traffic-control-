using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace WebApplication2.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.Email model )
        {
            using (MailMessage massege = new MailMessage(model.Gmail,model.TO)) {
                massege.Subject = model.Subject;
                massege.Body = model.Body;
                massege.IsBodyHtml = false;
                using(SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Netcore = new NetworkCredential(model.Gmail, model.password);
                    smtp.UseDefaultCredentials = true ;
                    smtp.Credentials= Netcore;
                    smtp.Port = 587;
                    smtp.Send(massege);
                    ViewBag.Massege = "Emailsent";
                }
            }
            return View();
        }

    }
}
