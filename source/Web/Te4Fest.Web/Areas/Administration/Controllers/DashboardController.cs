namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;

    public class DashboardController : AdministrationController
    {
        public DashboardController(IUsersDataService userData) 
            : base(userData)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}