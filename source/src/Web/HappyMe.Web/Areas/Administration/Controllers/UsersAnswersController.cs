namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.UsersAnswers;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MoreDotNet.Extensions.Common;

    public class UsersAnswersController :
        MvcGridAdministrationReadAndDeleteController<UserAnswer, UserAnswerGridViewModel>
    {
        public UsersAnswersController(
            IUsersDataService userData,
            IUsersAnswersAdministrationService dataRepository,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData, dataRepository, mappingService, userManager)
        {
        }

        public async Task<IActionResult> Index()
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
                        this.AdministrationService.As<IUsersAnswersAdministrationService>()
                            ?.GetChildrenAnswers(await this.GetUserIdAsync()))
                    .OrderBy(m => m.CreatedOn);
            }

            return this.View(usersAnswers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв отговор.");
            }

            var userHasRight =
                this.User.IsAdmin() ||
                (this.AdministrationService.As<IUsersAnswersAdministrationService>()?.CheckIfUserHasRights(
                    await this.GetUserIdAsync(),
                    id.Value) ?? false);

            if (userHasRight)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте потребителски отговор");
                return this.RedirectToAction(nameof(this.Index), "UsersAnswers", new { area = "Administration" });
            }

            this.TempData.AddDangerMessage("Нямате права за да изтриете потребителският отговор");
            return this.RedirectToAction(nameof(this.Index), "UsersAnswers", new { area = "Administration" });
        }
    }
}
