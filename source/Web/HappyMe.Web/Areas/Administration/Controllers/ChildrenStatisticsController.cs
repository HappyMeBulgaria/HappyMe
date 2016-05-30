namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

    public class ChildrenStatisticsController : AdministrationController
    {
        public ChildrenStatisticsController(IUsersDataService userData) 
            : base(userData)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}