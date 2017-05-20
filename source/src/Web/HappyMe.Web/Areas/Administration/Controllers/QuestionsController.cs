namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Questions;
    using HappyMe.Web.Areas.Administration.ViewModels.Questions;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MoreDotNet.Extensions.Common;

    public class QuestionsController :
        MvcGridAdministrationCrudController<Question, QuestionGridViewModel, QuestionCreateInputModel, QuestionUpdateInputModel>
    {
        private readonly IModulesAdministrationService modulesAdministrationService;
        private readonly IImagesAdministrationService imagesAdministrationService;

        public QuestionsController(
            IUsersDataService userData,
            IQuestionsAdministrationService questionsAdministrationService,
            IMappingService mappingService,
            IModulesAdministrationService modulesAdministrationService,
            IImagesAdministrationService imagesAdministrationService,
            UserManager<User> userManager)
            : base(userData, questionsAdministrationService, mappingService, userManager)
        {
            this.modulesAdministrationService = modulesAdministrationService;
            this.imagesAdministrationService = imagesAdministrationService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<QuestionGridViewModel> questions;
            if (this.User.IsAdmin())
            {
                questions = this.GetData().OrderBy(x => x.Id);
            }
            else
            {
                questions =
                    this.MappingService.MapCollection<QuestionGridViewModel>(
                        (this.AdministrationService as IQuestionsAdministrationService)
                            ?.GetUserQuestions(await this.GetUserIdAsync()))
                            .OrderBy(m => m.Id);
            }

            return this.View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await this.PopulateViewBag(null);
            return this.View(new QuestionCreateInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.AuthorId = await this.GetUserIdAsync();
                var imageData = model.ImageData;

                if (imageData != null)
                {
                    model.ImageId = this.imagesAdministrationService.Create(imageData.ToByteArray(), await this.GetUserIdAsync()).Id;
                }

                var entity = this.MappingService.Map<Question>(model);
                var allSelectedModules = this.modulesAdministrationService.GetAllByIds(model.ModulesIds);

                foreach (var selectedModule in allSelectedModules)
                {
                    entity.QuestionsInModules.Add(new QuestionInModule
                    {
                        Question = entity,
                        Module = selectedModule
                    });
                }

                this.AdministrationService.Create(entity);

                this.TempData.AddSuccessMessage("Успешно създадохте въпрос");
                return this.RedirectToIndex();
            }

            await this.PopulateViewBag(model.ModulesIds);
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв въпрос.");
            }

            var userHasRights = await this.CheckIfUserHasRightsForQuestion(id.Value);

            if (userHasRights)
            {
                var question = this.GetEditModelData(id);
                await this.PopulateViewBag(question.ModulesIds);

                return this.View(question);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този въпрос");
            return this.RedirectToIndex();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(QuestionUpdateInputModel model)
        {
            var userHasRights = await this.CheckIfUserHasRightsForQuestion(model.Id);

            if (userHasRights)
            {
                if (this.ModelState.IsValid)
                {
                    var imageData = model.ImageData;

                    if (imageData != null)
                    {
                        model.ImageId = this.imagesAdministrationService.Create(imageData.ToByteArray(), await this.GetUserIdAsync()).Id;
                    }

                    var entity = this.AdministrationService.Get(model.Id);
                    this.MappingService.Map(model, entity);
                    var allSelectedModules = this.modulesAdministrationService.GetAllByIds(model.ModulesIds);
                    entity.QuestionsInModules.Clear();

                    foreach (var selectedModule in allSelectedModules)
                    {
                        entity.QuestionsInModules.Add(new QuestionInModule
                        {
                            QuestionId = entity.Id,
                            ModuleId = selectedModule.Id
                        });
                    }

                    this.AdministrationService.Update(entity);

                    this.TempData.AddSuccessMessage("Успешно редактирахте въпрос");
                    return this.RedirectToIndex();
                }

                await this.PopulateViewBag(model.ModulesIds);
                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този въпрос");
            return this.RedirectToIndex();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв въпрос.");
            }

            var userHasRights = await this.CheckIfUserHasRightsForQuestion(id.Value);

            if (userHasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте въпрос");
                return this.RedirectToIndex();
            }

            this.TempData.AddDangerMessage("Нямате права за да изтриете този въпрос");
            return this.RedirectToIndex();
        }

        private async Task PopulateViewBag(int[] selectedIds)
        {
            var modules = this.User.IsAdmin()
                ? this.modulesAdministrationService.Read()
                : this.modulesAdministrationService.GetUserModules(await this.GetUserIdAsync());

            var projectedModules = modules
                .Select(x => new { Text = x.Name, Value = x.Id })
                .ToList();

            this.ViewBag.ModulesIdsData = new MultiSelectList(projectedModules, "Value", "Text", selectedIds);
        }

        private async Task<bool> CheckIfUserHasRightsForQuestion(int questionId)
        {
            return this.User.IsAdmin()
                                || (this.AdministrationService.As<IQuestionsAdministrationService>()
                                       ?.CheckIfUserIsAuthorOnQuestion(await this.GetUserIdAsync(), questionId) ?? false);
        }

        private RedirectToActionResult RedirectToIndex()
        {
            return this.RedirectToAction(nameof(this.Index), "Questions", new { area = "Administration" });
        }
    }
}
