using Microsoft.AspNetCore.Mvc;

namespace BENETWeb_072025.Controllers
{
    public class UserController : Controller
    {
        [ActionName("Login")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
