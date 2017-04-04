namespace HappyMe.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class AnswersDataService : IAnswersDataService
    {
        private readonly IRepository<UserAnswer> userAnswersRepository;
        private readonly IRepository<Answer> answersRepository;

        public AnswersDataService(
            IRepository<UserAnswer> userAnswersRepository,
            IRepository<Answer> answersRepository)
        {
            this.userAnswersRepository = userAnswersRepository;
            this.answersRepository = answersRepository;
        }

        public async Task AnswerAsUser(string userId, int answerId, int moduleSessionId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User id must be non-null");
            }

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

        ////public async Task<IEnumerable<Answer>> GetRandomAnswersForQuestion(Question question, int answersCount)
        ////{
        ////    var correctAnswersForQuestion = question.Answers
        ////        .Where(x => x.IsCorrect)
        ////        .ToList();

        ////    if (correctAnswersForQuestion == null || !correctAnswersForQuestion.Any())
        ////    {
        ////        throw new ArgumentException("Question must have atleast one correct answer!", nameof(question));
        ////    }

        ////    if (question.Type == QuestionType.ColorQuestion)
        ////    {
        ////        return null;
        ////    }

        ////    var randomAnswers =
        ////        this.answersRepository.All()
        ////            .Where(x => x.Question.Type == question.Type
        ////            && correctAnswersForQuestion.All(y => x.Id != y.Id))
        ////            .OrderBy(x => new Guid())
        ////            .Take(answersCount)
        ////            .ToListAsync();

        ////    return await randomAnswers;
        ////}
    }
}
