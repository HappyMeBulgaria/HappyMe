namespace HappyMe.Web.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Controllers.Base;

    public class FeedbackController : BaseController
    {
        ////private readonly IFeedbackDataService feedbackDataService;

        ////public FeedbackController(IFeedbackDataService feedbackDataService)
        ////{
        ////    this.feedbackDataService = feedbackDataService;
        ////}

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Send()
        {
            return this.View();
        }

        public ActionResult Success()
        {
            return this.View();
        }
    }
}
