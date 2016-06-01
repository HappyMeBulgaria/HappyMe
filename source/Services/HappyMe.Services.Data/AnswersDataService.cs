namespace HappyMe.Services.Data
{
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class AnswersDataService : IAnswersDataService
    {
        private readonly IRepository<UserAnswer> userAnswersRepository; 

        public AnswersDataService(IRepository<UserAnswer> userAnswersRepository)
        {
            this.userAnswersRepository = userAnswersRepository;
        }

        public async Task AnswerAsUser(string userId, int answerId, int moduleSessionId)
        {
            var newAnswer = new UserAnswer(userId, answerId, moduleSessionId);
            this.userAnswersRepository.Add(newAnswer);
            await this.userAnswersRepository.SaveChangesAsync();
        }

        public async Task AnswerAsAnonymous(int answerId, int moduleSessionId)
        {
            var newAnswer = new UserAnswer(answerId, moduleSessionId);
            this.userAnswersRepository.Add(newAnswer);
            await this.userAnswersRepository.SaveChangesAsync();
        }
    }
}
