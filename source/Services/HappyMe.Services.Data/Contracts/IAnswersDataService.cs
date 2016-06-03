namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using HappyMe.Services.Common;

    public interface IAnswersDataService : IService
    {
        Task AnswerAsUser(string userId, int answerId, int moduleSessionId);

        Task AnswerAsAnonymous(int answerId, int moduleSessionId);
    }
}
