using Microsoft.AspNetCore.Mvc;
using MVCPract2Kurs.Models;
using System.Diagnostics;

namespace MVCPract2Kurs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return RedirectToAction("Register", "Account");
        }

        public IActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ForgotPassword()
        {
            return RedirectToAction("ForgotPassword", "Account");
        }

    }
}
