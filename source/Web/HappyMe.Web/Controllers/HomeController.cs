namespace HappyMe.Web.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Common.Mapping.Contracts;

    public class HomeController : Controller
    {
        private IMappingService mappingService;

        public HomeController(IMappingService mappingService)
        {
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}