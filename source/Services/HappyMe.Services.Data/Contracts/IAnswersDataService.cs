namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IAnswersDataService
    {
        Task AnswerAsUser(string userId, int answerId, int moduleSessionId);

        Task AnswerAsAnonymous(int answerId, int moduleSessionId);
    }
}
