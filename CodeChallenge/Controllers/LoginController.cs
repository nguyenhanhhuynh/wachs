using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
