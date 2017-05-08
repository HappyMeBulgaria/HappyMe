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
            return this.View();
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application ge.";
            
            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
