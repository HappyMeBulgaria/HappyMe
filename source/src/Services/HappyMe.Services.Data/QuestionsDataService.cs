namespace HappyMe.Services.Data
{
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class QuestionsDataService : IQuestionsDataService
    {
        private readonly IRepository<Question> questionRepository;

        public QuestionsDataService(IRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public bool IsCorrectAnswer(int questionId, int answerId) => 
            this.questionRepository
                .All()
                .Any(q => 
                    q.Id == questionId && 
                    q.Answers.Any(a => a.IsCorrect && a.Id == answerId));
    }
}
