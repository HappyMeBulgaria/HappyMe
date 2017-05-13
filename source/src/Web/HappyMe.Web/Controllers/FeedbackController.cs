namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.InputModels.Feedback;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FeedbackController : BaseController
    {
        private readonly IFeedbackDataService feedbackDataService;

        public FeedbackController(IFeedbackDataService feedbackDataService, UserManager<User> userManager)
            : base(userManager)
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

            this.TempData.AddWarningMessage("Невалидни данни.");

            return this.RedirectToAction<HomeController>(c => c.Index());
        }

        [HttpGet]
        public ActionResult Success()
        {
            return this.View();
        }
    }
}
