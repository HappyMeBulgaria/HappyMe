namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.InputModels.Questions;

    using Microsoft.AspNet.Identity;

    public class QuestionsController : BaseController
    {
        private readonly IQuestionsDataService questionsDataService;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly IAnswersDataService answersDataService;

        public QuestionsController(
            IModuleSessionDataService moduleSessionDataService,
            IAnswersDataService answersDataService,
            IQuestionsDataService questionsDataService)
        {
            this.moduleSessionDataService = moduleSessionDataService;
            this.answersDataService = answersDataService;
            this.questionsDataService = questionsDataService;
        }

        [HttpGet]
        public ActionResult Answer(int? id)
        {
            if (!id.HasValue)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            var module = this.moduleSessionDataService.GetById(id.Value);

            if (module == null)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            // To VM
            var nextQuestion = this.moduleSessionDataService.NextQuestion(id, this.User.Identity.GetUserId());

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Answer(AnswerQuestionInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                if (this.User.IsLoggedIn())
                {
                    await this.answersDataService.AnswerAsUser(
                        this.User.Identity.GetUserId(),
                        inputModel.AnswerId,
                        inputModel.SessionId);
                }
                else
                {
                    await this.answersDataService.AnswerAsAnonymous(inputModel.AnswerId, inputModel.SessionId);
                }

                var isAnswerCorrect = this.questionsDataService.IsCorrectAnswer(inputModel.QuestionId, inputModel.AnswerId);

                if (isAnswerCorrect)
                {
                    return this.RedirectToAction(
                        "Answer",
                        "Questions",
                        new { area = string.Empty, id = inputModel.SessionId });
                }
                else
                {
                    // incorrect answer, try again
                    return this.View(inputModel);
                }
            }

            return this.View(inputModel);
        }
    }
}