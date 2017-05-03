namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.InputModels.Feedback;

    using Resource = Resources.Feedback.FeedbackController;

    public class FeedbackController : BaseController
    {
        private readonly IFeedbackDataService feedbackDataService;

        public FeedbackController(IFeedbackDataService feedbackDataService)
        {
            this.feedbackDataService = feedbackDataService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Send(FeedbackInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                await this.feedbackDataService.Add(model.Name, model.Email, model.Subject, model.Message);

                return this.RedirectToAction(nameof(this.Success));
            }

            this.TempData.AddWarningMessage(Resource.Invalid_feedback_error);

            return this.RedirectToAction<HomeController>(c => c.Index());
        }

        [HttpGet]
        public ActionResult Success()
        {
            return this.View();
        }
    }
}
