namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;

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