using BookStore.Models;
using BookStore.Utils;
using Microsoft.AspNetCore.Mvc;
using AppContext = BookStore.Models.AppContext;

namespace BookStore.Controllers
{
    public class LoginController : Controller
    {
        AppContext db = new AppContext();

        [HttpPost]
        public IActionResult Login(Auth auth)
        {
            if (auth.login == null || auth.password == null)
                return View();

            string password = MD5.GetHashString(auth.password);
            List<Customer> customers = db.Customer.ToList();
            Customer customer = customers.Find(c => c.password.Trim().Equals(password) && c.login.Trim().Equals(auth.login));


            if (customer != null)
                return View("/Views/Login/Authorizied.cshtml");

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }
    }
}
