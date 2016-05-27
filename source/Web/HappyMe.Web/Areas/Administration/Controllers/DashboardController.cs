namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

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