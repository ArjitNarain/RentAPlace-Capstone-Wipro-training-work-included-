using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WiproWebApp.Models;

namespace WiproWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to Web Development";
            ViewData["Display"] = "This course should teach you to build a web application";
            TempData["Show"] = "Can see the message!";
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
