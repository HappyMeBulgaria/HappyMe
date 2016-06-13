namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.Feedback;
    using HappyMe.Web.Common.Extensions;

    public class FeedbackController : MvcGridAdministrationReadAndDeleteController<Feedback, FeedbackGridViewModel>
    {
        public FeedbackController(
            IUsersDataService userData, 
            IAdministrationService<Feedback> dataRepository, 
            IMappingService mappingService) 
            : base(userData, dataRepository, mappingService)
        {
        }
        
        public ActionResult Index() => this.View(this.GetData().OrderBy(f => f.Id));

        public ActionResult Delete(int id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте потребителски отговор");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}