using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _5_1_stackoverflow.data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace _5_1_stackoverflow.web.Controllers
{
    public class AccountController : Controller
    {
        private string _connString;

        public AccountController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user, string password)
        {
            Repository rep = new Repository(_connString);
            rep.AddUser(user, password);
            return Redirect("/account/login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            Repository rep = new Repository(_connString);
            User user = rep.Login(email, password);
            if(user == null)
            {
                return RedirectToAction("Login");
            }

            var claims = new List<Claim>
                {
                    new Claim("user", email)
                };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}