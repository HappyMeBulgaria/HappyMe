namespace HappyMe.Services.Data
{
    using System.Linq;

    using HappyMe.Common.Tools;
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    using MoreDotNet.Extentions.Common;

    public class ModuleSessionDataService : IModuleSessionDataService
    {
        private readonly IRepository<ModuleSession> moduleSessionsRepository;
        private readonly IRepository<UserAnswer> userAnswersRepository;

        public ModuleSessionDataService(
            IRepository<ModuleSession> moduleSessionsRepository,
            IRepository<UserAnswer> userAnswersRepository)
        {
            this.moduleSessionsRepository = moduleSessionsRepository;
            this.userAnswersRepository = userAnswersRepository;
        }

        public Question NextQuestion(int moduleSessionId, string userId)
        {
            var answerdQuestioIds =
                this.userAnswersRepository.All()
                    .Where(x => x.ModuleInstanceId == moduleSessionId && x.UserId == userId && x.Answer.IsCorrect)
                    .Select(x => x.Answer.QuestionId);

            var unanswerdQuestions =
                this.moduleSessionsRepository
                    .GetById(moduleSessionId)
                    .Module.Questions.Where(x => !answerdQuestioIds.Contains(x.Id))
                    .ToArray();

            if (!unanswerdQuestions.Any())
            {
                this.moduleSessionsRepository.GetById(moduleSessionId).IsFinised = true;
                this.moduleSessionsRepository.SaveChanges();
                return null;
            }

            return RandomGenerator.Instance.OneOf(unanswerdQuestions);
        }
    }
}
