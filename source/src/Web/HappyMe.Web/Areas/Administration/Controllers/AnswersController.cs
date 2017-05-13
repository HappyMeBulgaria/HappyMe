namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Common.Extensions;
    using Data.Models;

    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

    using InputModels.Answers;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MoreDotNet.Extensions.Common;

    using ViewModels.Answers;

    public class AnswersController :
        MvcGridAdministrationCrudController<Answer, AnswerGridViewModel, AnswerCreateInputModel, AnswerUpdateInputModel>
    {
        private readonly IImagesAdministrationService imagesAdministrationService;
        private readonly IQuestionsAdministrationService questionsAdministrationService;

        public AnswersController(
            IUsersDataService userData,
            IAnswersAdministrationService answersAdministrationService,
            IMappingService mappingService,
            IImagesAdministrationService imagesAdministrationService,
            IQuestionsAdministrationService questionsAdministrationService,
            UserManager<User> userManager)
            : base(userData, answersAdministrationService, mappingService, userManager)
        {
            this.imagesAdministrationService = imagesAdministrationService;
            this.questionsAdministrationService = questionsAdministrationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AnswerGridViewModel> answers;
            if (this.User.IsAdmin())
            {
                answers = this.GetData().OrderBy(x => x.Id);
            }
            else
            {
                answers = this.MappingService
                    .MapCollection<AnswerGridViewModel>(
                        this.AdministrationService.As<IAnswersAdministrationService>()
                        ?.GetAllUserAnswers(await this.GetUserIdAsync()))
                    .OrderBy(m => m.Id);
            }

            return this.View(answers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await this.PopulateViewBag(null);
            return this.View(new AnswerCreateInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnswerCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.AuthorId = await this.GetUserIdAsync();

                var imageData = model.ImageData;
                if (imageData != null)
                {
                    model.ImageId = this.imagesAdministrationService.Create(imageData.ToByteArray(), await this.GetUserIdAsync()).Id;
                }

                var entity = this.BaseCreate(model);
                if (entity != null)
                {
                    this.TempData.AddSuccessMessage("Успешно създадохте отговор");
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв отговор.");
            }

            var hasRights = await this.CheckIfUserHasRightsForAnswer(id.Value);

            if (hasRights)
            {
                var answer = this.GetEditModelData(id);
                await this.PopulateViewBag(answer.QuestionId);
                return this.View(answer);
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnswerUpdateInputModel model)
        {
            var hasRights = await this.CheckIfUserHasRightsForAnswer(model.Id);

            if (hasRights)
            {
                if (this.ModelState.IsValid)
                {
                    var imageData = model.ImageData;
                    if (imageData != null)
                    {
                        model.ImageId = this.imagesAdministrationService.Create(imageData.ToByteArray(), await this.GetUserIdAsync()).Id;
                    }

                    var entity = this.BaseUpdate(model, model.Id);
                    if (entity != null)
                    {
                        this.TempData.AddSuccessMessage("Успешно редактирахте отговор");
                        return this.RedirectToAction(nameof(this.Index));
                    }
                }

                await this.PopulateViewBag(model.QuestionId);
                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв отговор.");
            }

            var hasRights = await this.CheckIfUserHasRightsForAnswer(id.Value);

            if (hasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте отговор");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате право да изтриете този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task PopulateViewBag(int? id)
        {
            var questions = this.User.IsAdmin()
                ? this.questionsAdministrationService.Read()
                : this.questionsAdministrationService.GetUserQuestions(await this.GetUserIdAsync());

            this.ViewBag.QuestionIdData =
                questions.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString(), Selected = m.Id == id });
        }

        private async Task<bool> CheckIfUserHasRightsForAnswer(int answerId)
        {
            return this.User.IsAdmin() ||
                ((this.AdministrationService as IAnswersAdministrationService)
                    ?.CheckIfUserIsAnswerAuthor(answerId, await this.GetUserIdAsync()) ?? false);
        }
    }
}
