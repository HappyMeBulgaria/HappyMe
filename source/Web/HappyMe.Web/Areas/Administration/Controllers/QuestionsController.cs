using System;
using System.Collections.Generic;
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

namespace HappyMe.Web.Areas.Administration.Controllers
{
    public class QuestionsController : 
        MvcGridAdministrationController<Question, QuestionGridViewModel, QuestionCreateInputModel, QuestionUpdateInputModel>
    {
        public QuestionsController(
            IUsersDataService userData,
            IAdministrationService<Question> dataRepository, 
            IMappingService mappingService) 
            : base(userData, dataRepository, mappingService)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(x => x.Id));

        [HttpGet]
        public ActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreateInputModel model)
        {
            model.AuthorId = this.UserProfile.Id;
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
    }
}