namespace HappyMe.Services.Data
{
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class QuestionsDataService : IQuestionsDataService
    {
        private readonly IRepository<Question> questionRepository;

        public QuestionsDataService(IRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public bool IsCorrectAnswer(int questionId, int answerId)
        {
            return this.questionRepository.GetById(questionId).Answers.Any(x => x.IsCorrect && x.Id == answerId);
        }
    }
}
