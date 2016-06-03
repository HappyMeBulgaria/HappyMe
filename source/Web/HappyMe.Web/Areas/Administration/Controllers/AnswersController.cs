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
                        (this.AdministrationService as IAnswersAdministrationService)
                            .GetAllUserAnswers(this.UserProfile.Id))
                    .OrderBy(m => m.Id);
            }

            return this.View(answers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.PopulateViewBag();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerCreateInputModel model)
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

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var hasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IAnswersAdministrationService)
                    .CheckIfUserIsAnswerAuthor(id, this.UserProfile.Id);

            if (hasRights)
            {
                this.PopulateViewBag();
                return this.View(this.GetEditModelData(id));
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AnswerUpdateInputModel model)
        {
            var hasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IAnswersAdministrationService)
                    .CheckIfUserIsAnswerAuthor(model.Id, this.UserProfile.Id);

            if (hasRights)
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

                this.PopulateViewBag();
                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате право да редактирате този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var hasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IAnswersAdministrationService)
                    .CheckIfUserIsAnswerAuthor(id, this.UserProfile.Id);

            if (hasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте отговор");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате право да изтриете този отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBag()
        {
            var questions = this.User.IsAdmin()
                ? this.questionsAdministrationService.Read()
                : this.questionsAdministrationService.GetUserQuestions(this.UserProfile.Id);

            this.ViewBag.QuestionIdData = 
                questions.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString() });
        }
    }
}