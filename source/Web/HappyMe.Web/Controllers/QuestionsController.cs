namespace HappyMe.Web.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Controllers.Base;

    public class QuestionsController : BaseController
    {
        private readonly IQuestionsDataService questionsDataService;

        public QuestionsController(IQuestionsDataService questionsDataService)
        {
            this.questionsDataService = questionsDataService;
        }

        [HttpPost]
        public ActionResult Answer()
        {
            return this.View();
        }
    }
}