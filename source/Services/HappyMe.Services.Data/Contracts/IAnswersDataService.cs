namespace HappyMe.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HappyMe.Common.Models;
    using HappyMe.Data.Models;
    using HappyMe.Services.Common;

    public interface IAnswersDataService : IService
    {
        Task AnswerAsUser(string userId, int answerId, int moduleSessionId);

        Task AnswerAsAnonymous(int answerId, int moduleSessionId);

        ////Task<IEnumerable<Answer>> GetRandomAnswersForQuestion(Question question, int answersCount);
    }
}
