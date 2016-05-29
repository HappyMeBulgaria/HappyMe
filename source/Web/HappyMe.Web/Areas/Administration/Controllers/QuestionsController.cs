using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyMe.Data.Models;
using HappyMe.Services.Administration.Contracts;
using HappyMe.Services.Common.Mapping.Contracts;
using HappyMe.Services.Data.Contracts;
using HappyMe.Web.Areas.Administration.Controllers.Base;
using HappyMe.Web.Areas.Administration.InputModels.Questions;
using HappyMe.Web.Areas.Administration.ViewModels.Questions;
using HappyMe.Web.Common.Extensions;
using MoreDotNet.Extentions.Common;

namespace HappyMe.Web.Areas.Administration.Controllers
{
    public class QuestionsController : 
        MvcGridAdministrationCrudController<Question, QuestionGridViewModel, QuestionCreateInputModel, QuestionUpdateInputModel>
    {
        private readonly IModulesAdministrationService modulesAdministrationService;
        private readonly IImagesAdministrationService imagesAdministrationService;

        public QuestionsController(
            IUsersDataService userData,
            IAdministrationService<Question> questionsAdministrationService, 
            IMappingService mappingService,
            IModulesAdministrationService modulesAdministrationService,
            IImagesAdministrationService imagesAdministrationService) 
            : base(userData, questionsAdministrationService, mappingService)
        {
            this.modulesAdministrationService = modulesAdministrationService;
            this.imagesAdministrationService = imagesAdministrationService;
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(x => x.Id));

        [HttpGet]
        public ActionResult Create()
        {
            // get modules for dropdown
            this.PopulateViewBag();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreateInputModel model)
        {
            // setting author
            model.AuthorId = this.UserProfile.Id;

            // setting image
            var imageData = model.ImageData;
            var target = new MemoryStream();
            imageData.InputStream.CopyTo(target);
            var data = target.ToByteArray();
            model.ImageId = this.imagesAdministrationService.Create(data, this.UserProfile.Id).Id;

            var entity = this.BaseCreate(model);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно създадохте въпрос");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(int id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(QuestionUpdateInputModel model)
        {
            var entity = this.BaseUpdate(model, model.Id);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно редактирахте въпрос");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте въпрос");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBag()
        {
            this.ViewBag.ModuleIdData =
                this.modulesAdministrationService.GetUserAndPublicModules(this.UserProfile.Id)
                    .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() });
        }
    }
}