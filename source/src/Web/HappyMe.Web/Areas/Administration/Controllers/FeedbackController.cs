namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.Feedback;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FeedbackController : MvcGridAdministrationReadAndDeleteController<Feedback, FeedbackGridViewModel>
    {
        public FeedbackController(
            IUsersDataService userData,
            IAdministrationService<Feedback> dataRepository,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData, dataRepository, mappingService, userManager)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(f => f.Id));

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такава обратна връзка.");
            }

            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте потребителски отговор");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
