namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Questions;
    using HappyMe.Web.Areas.Administration.ViewModels.Questions;
    using HappyMe.Web.Common.Extensions;

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
            IImagesAdministrationService imagesAdministrationService) 
            : base(userData, questionsAdministrationService, mappingService)
        {
            this.modulesAdministrationService = modulesAdministrationService;
            this.imagesAdministrationService = imagesAdministrationService;
        }

        public ActionResult Index()
        {
            IEnumerable<QuestionGridViewModel> questions;
            if (this.User.IsAdmin())
            {
                questions = this.GetData().OrderBy(x => x.Id);
            }
            else
            {
                questions = this.MappingService
                    .MapCollection<QuestionGridViewModel>(
                        (this.AdministrationService as IQuestionsAdministrationService)
                            .GetUserQuestions(this.UserProfile.Id))
                    .OrderBy(m => m.Id);
            }

            return this.View(questions);
        } 

        [HttpGet]
        public ActionResult Create()
        {
            this.PopulateViewBag(null);
            return this.View(new QuestionCreateInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreateInputModel model)
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
                this.TempData.AddSuccessMessage("Успешно създадохте въпрос");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.PopulateViewBag(null);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var userHasRights = this.User.IsAdmin() ||
                 (this.AdministrationService as IQuestionsAdministrationService)
                    .CheckIfUserIsAuthorOnQuestion(this.UserProfile.Id, id);

            if (userHasRights)
            {
                var question = this.GetEditModelData(id);
                this.PopulateViewBag(question.ModuleId);

                return this.View(question);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този въпрос");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(QuestionUpdateInputModel model)
        {
            var userHasRights = this.User.IsAdmin() ||
                 (this.AdministrationService as IQuestionsAdministrationService)
                    .CheckIfUserIsAuthorOnQuestion(this.UserProfile.Id, model.Id);

            if (userHasRights)
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
                    this.TempData.AddSuccessMessage("Успешно редактирахте въпрос");
                    return this.RedirectToAction(nameof(this.Index));
                }

                this.PopulateViewBag(model.ModuleId);
                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този въпрос");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userHasRights = this.User.IsAdmin() ||
                 (this.AdministrationService as IQuestionsAdministrationService)
                    .CheckIfUserIsAuthorOnQuestion(this.UserProfile.Id, id);

            if (userHasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте въпрос");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате права за да изтриете този въпрос");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBag(int? id)
        {
            var modules = this.User.IsAdmin() 
                ? this.modulesAdministrationService.Read() 
                : this.modulesAdministrationService.GetUserModules(this.UserProfile.Id);

            this.ViewBag.ModuleIdData = modules.Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString(), Selected = m.Id == id });          
        }
    }
}