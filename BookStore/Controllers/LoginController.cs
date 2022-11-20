using BookStore.Models;
using BookStore.Utils;
using Microsoft.AspNetCore.Mvc;
using AppContext = BookStore.Models.AppContext;

namespace BookStore.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        public IActionResult Login(Auth auth)
        {
            if (HttpContext.Session.GetString("UserName") != null)
                return RedirectToAction("Login");

            if (auth.login == null || auth.password == null)
                return View();

            string password = MD5.GetHashString(auth.password);

            using (AppContext db = new AppContext()) 
            {
                List<Customer> customers = db.Customer.ToList();
                Customer customer = customers.Find(c => c.password.Trim().Equals(password) && c.login.Trim().Equals(auth.login));


                if (customer != null)
                {
                    HttpContext.Session.SetString("UserName", auth.login);
                    return View("/Views/Authorized/Authorizied.cshtml");
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }
    }
}
