using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Te4Fest.Data.Models;
using Te4Fest.Services.Administration.Contracts;
using Te4Fest.Services.Common.Mapping.Contracts;
using Te4Fest.Web.Areas.Administration.Controllers.Base;
using Te4Fest.Web.Areas.Administration.InputModels.Questions;
using Te4Fest.Web.Areas.Administration.ViewModels.Questions;

namespace Te4Fest.Web.Areas.Administration.Controllers
{
    public class QuestionsController : 
        MvcGridAdministrationController<Question, QuestionGridViewModel, QuestionCreateInputModel, QuestionUpdateInputModel>
    {
        public QuestionsController(
            IAdministrationService<Question> dataRepository, 
            IMappingService mappingService) 
            : base(dataRepository, mappingService)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(x => x.Id));
    }
}