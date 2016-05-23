namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Web.Areas.Administration.Controllers.Base;

    public class DashboardController : AdministrationController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}