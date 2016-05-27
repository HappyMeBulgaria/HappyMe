namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

    public class AnswersController : AdministrationController
    {
        public AnswersController(IUsersDataService userData) 
            : base(userData)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}