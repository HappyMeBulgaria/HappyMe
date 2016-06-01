namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Common.Extensions;
    using Data.Models;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using InputModels.Answers;
    using InputModels.Questions;
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
            IAdministrationService<Answer> answersAdministrationService,
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
            return this.View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            // get questions for dropdown
            this.PopulateViewBag();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerCreateInputModel model)
        {
            // setting author
            model.AuthorId = this.UserProfile.Id;

            // setting image if any
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
            // get questions for dropdown
            this.PopulateViewBag();

            return this.View(this.GetEditModelData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AnswerUpdateInputModel model)
        {
            // updating image if any
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

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте отговор");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBag()
        {
            this.ViewBag.ModuleIdData =
                this.questionsAdministrationService.GetUserAndPublicQuestions(this.UserProfile.Id)
                    .Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString() });
        }
    }
}