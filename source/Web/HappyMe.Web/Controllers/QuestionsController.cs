namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.InputModels.Questions;
    using HappyMe.Web.ViewModels.Questions;

    using Microsoft.AspNet.Identity;

    public class QuestionsController : BaseController
    {
        private readonly IQuestionsDataService questionsDataService;
        private readonly IMappingService mappingService;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly IAnswersDataService answersDataService;

        public QuestionsController(
            IModuleSessionDataService moduleSessionDataService,
            IAnswersDataService answersDataService,
            IQuestionsDataService questionsDataService,
            IMappingService mappingService)
        {
            this.moduleSessionDataService = moduleSessionDataService;
            this.answersDataService = answersDataService;
            this.questionsDataService = questionsDataService;
            this.mappingService = mappingService;
        }

        [HttpGet]
        public ActionResult Answer(int? id)
        {
            if (!id.HasValue)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            var session = this.moduleSessionDataService.GetById(id.Value);

            if (session == null)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            // To VM
            var nextQuestion = this.mappingService.Map<QuestionViewModel>(
                this.moduleSessionDataService.NextQuestion(id.Value, this.User.Identity.GetUserId()));

            if (nextQuestion == null)
            {
                // No more question in module
                this.moduleSessionDataService.FinishSession(session.Id);
                return this.RedirectToAction("Success", "Modules", new { area = string.Empty });
            }

            nextQuestion.SessionId = id.Value;
            var type = nextQuestion.Type.ToString();

            return this.View(type, nextQuestion);
        }

        [HttpPost]
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

                return this.JsonSuccess(new
                {
                    IsAnswerCorrect = isAnswerCorrect,
                    inputModel.SessionId
                });
            }

            return this.JsonError("Упс, възникна грешка!");
        }
    }
}