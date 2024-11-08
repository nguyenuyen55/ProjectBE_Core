using Microsoft.AspNetCore.Mvc;

namespace WebNETCORE.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
