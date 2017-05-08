using System.Diagnostics;
using HappyMe.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HappyMe.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IEmailSender emailSender, ISmsSender smsSender)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application ge.";
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
