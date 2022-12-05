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
                        //string s = reader.GetString(0);
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

        public IActionResult Registration(Customer customer)
        {
            if(customer.id == null || customer.number_phone==null || customer.password ==  null || customer.number_phone == null)
            {
                ModelState.AddModelError("", "Fill all required fields");
                return View("~/Views/Home/Login.cshtml");
            }

            using (NpgsqlConnection conn = new NpgsqlConnection())
            {
                conn.ConnectionString = ConnectionString.GetString();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select id from customer where login=\'" + customer.login + "\';";
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        ModelState.AddModelError("", "This login is already used. Choose another one");
                        return View("~/Views/Home/Login.cshtml");
                    }
                }

                cmd.CommandText = "select id from customer where mail=\'" + customer.login + "\';";
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        ModelState.AddModelError("", "This mail is already used. Choose another one");
                        return View("~/Views/Home/Login.cshtml");
                    }
                }

                string password = MD5.GetHashString(customer.password);
                cmd.CommandText = $"insert into customer (login, password, mail, number_phone) values (\'{customer.login}\', \'{password}\', \'{customer.mail}\', \'{customer.number_phone}\');";
                cmd.ExecuteNonQuery();
            }

            HttpContext.Session.SetString("UserName", customer.login);
            return View("/Views/Authorized/Authorizied.cshtml");
        }
    }
}