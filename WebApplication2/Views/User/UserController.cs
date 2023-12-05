using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Views.User
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
