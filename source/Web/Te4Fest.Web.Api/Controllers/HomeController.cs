namespace Te4Fest.Web.Api.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Title = "Home Page";

            return this.View();
        }
    }
}
