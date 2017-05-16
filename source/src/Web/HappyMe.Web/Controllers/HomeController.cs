namespace HappyMe.Web.Controllers
{
    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Web.Controllers.Base;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public HomeController(UserManager<User> userManager)
            : base(userManager)
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
