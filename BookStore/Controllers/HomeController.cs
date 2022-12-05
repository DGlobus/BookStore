using BookStore.Models;
using BookStore.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            string s = HttpContext.Session.GetString("UserName");
            if (s != null)
            {
                return View("~/Views/Authorized/Index.cshtml");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login");
        }

        public IActionResult Books()
        {
            return View("~/Views/Authorized/Books/Yours.cshtml");
        }

        public IActionResult Registration()
        {
            return View("~/Views/Home/Registration.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}