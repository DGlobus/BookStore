using BookStore.Models;
using BookStore.Utils;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
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
            bool customerExist = false;

            using (NpgsqlConnection conn = new NpgsqlConnection())
            {
                conn.ConnectionString = ConnectionString.GetString();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select id from customer where login=\'" + auth.login + "\' and password=\'" + password+"\';";
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        customerExist = true;
                    }
                }
            }

            if (customerExist)
            {
                HttpContext.Session.SetString("UserName", auth.login);
                return View("/Views/Authorized/Authorizied.cshtml");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View("~/Views/Home/Login.cshtml");
        }

        private bool DoesCustomerExist(List<Customer> customers, string login, string password)
        {
            return customers.Find(c => c.password.Trim().Equals(password) && c.login.Trim().Equals(login)) != null ? true : false;
        }
    }
}
