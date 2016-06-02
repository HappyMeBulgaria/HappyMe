namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.InputModels.Feedback;

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

            this.TempData.AddWarningMessage("Невалидна обратна връзка");

            // Redirect to lending page (or wherever is feedback form)
            return new EmptyResult();
        }

        public ActionResult Success()
        {
            return this.View();
        }
    }
}
