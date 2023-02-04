using Microsoft.AspNetCore.Mvc;

namespace Wordle.WebApp.Controllers
{
    public class HomeController :Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
