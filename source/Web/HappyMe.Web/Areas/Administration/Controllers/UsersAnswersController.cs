namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.UsersAnswers;
    using HappyMe.Web.Common.Extensions;

    public class UsersAnswersController : MvcGridAdministrationReadAndDeleteController<UserAnswer, UserAnswerGridViewModel>
    {
        public UsersAnswersController(
            IUsersDataService userData,
            IUsersAnswersAdministrationService dataRepository, 
            IMappingService mappingService) 
            : base(userData, dataRepository, mappingService)
        {
        }

        public ActionResult Index()
        {
            IEnumerable<UserAnswerGridViewModel> usersAnswers;
            if (this.User.IsAdmin())
            {
                usersAnswers = this.GetData().OrderBy(x => x.CreatedOn);
            }
            else
            {
                usersAnswers = this.MappingService
                    .MapCollection<UserAnswerGridViewModel>(
                        (this.AdministrationService as IUsersAnswersAdministrationService)
                            .GetChildrenAnswers(this.UserProfile.Id))
                    .OrderBy(m => m.CreatedOn);
            }

            return this.View(usersAnswers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, int secondId)
        {
            var userHasRight =
                this.User.IsAdmin() ||
                (this.AdministrationService as IUsersAnswersAdministrationService).CheckIfUserHasRights(
                    this.UserProfile.Id,
                    id,
                    secondId);
            if (userHasRight)
            {
                this.BaseDestroy(id, secondId);

                this.TempData.AddSuccessMessage("Успешно изтрихте потребителски отговор");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате права за да изтриете потребителският отговор");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}