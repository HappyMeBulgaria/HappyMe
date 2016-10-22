namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Common.Extensions;
    using Data.Models;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

    using InputModels.Answers;

    using MoreDotNet.Extensions.Common;

    using Services.Administration.Contracts;
    using Services.Common.Mapping.Contracts;
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
            IQuestionsAdministrationService questionsAdministrationService)
            : base(userData, answersAdministrationService, mappingService)
        {
            this.imagesAdministrationService = imagesAdministrationService;
            this.questionsAdministrationService = questionsAdministrationService;
        }

        [HttpGet]
        public ActionResult Index()
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
                            .GetAllUserAnswers(this.UserProfile.Id))
                    .OrderBy(m => m.Id);
            }

            return this.View(answers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.PopulateViewBag(null);
            return this.View(new AnswerCreateInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.AuthorId = this.UserProfile.Id;

                var imageData = model.ImageData;
                if (imageData != null)
                {
                    var target = new MemoryStream();
                    imageData.InputStream.CopyTo(target);
                    var data = target.ToArray();
                    model.ImageId = this.imagesAdministrationService.Create(data, this.UserProfile.Id).Id;
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
        public ActionResult Update(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв отговор.");
            }

            var hasRights = this.CheckIfUserHasRightsForAnswer(id.Value);

            if (hasRights)
            {
                var answer = this.GetEditModelData(id);
                this.PopulateViewBag(answer.QuestionId);
                return this.View(answer);
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AnswerUpdateInputModel model)
        {
            var hasRights = this.CheckIfUserHasRightsForAnswer(model.Id);

            if (hasRights)
            {
                if (this.ModelState.IsValid)
                {
                    var imageData = model.ImageData;
                    if (imageData != null)
                    {
                        var target = new MemoryStream();
                        imageData.InputStream.CopyTo(target);
                        var data = target.ToArray();
                        model.ImageId = this.imagesAdministrationService.Create(data, this.UserProfile.Id).Id;
                    }

                    var entity = this.BaseUpdate(model, model.Id);
                    if (entity != null)
                    {
                        this.TempData.AddSuccessMessage("Успешно редактирахте отговор");
                        return this.RedirectToAction(nameof(this.Index));
                    }
                }

                this.PopulateViewBag(model.QuestionId);
                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв отговор.");
            }

            var hasRights = this.CheckIfUserHasRightsForAnswer(id.Value);

            if (hasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте отговор");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате право да изтриете този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBag(int? id)
        {
            var questions = this.User.IsAdmin()
                ? this.questionsAdministrationService.Read()
                : this.questionsAdministrationService.GetUserQuestions(this.UserProfile.Id);

            this.ViewBag.QuestionIdData =
                questions.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString(), Selected = m.Id == id });
        }

        private bool CheckIfUserHasRightsForAnswer(int answerId)
        {
            return this.User.IsAdmin() ||
                this.AdministrationService.As<IAnswersAdministrationService>()
                    .CheckIfUserIsAnswerAuthor(answerId, this.UserProfile.Id);
        }
    }
}