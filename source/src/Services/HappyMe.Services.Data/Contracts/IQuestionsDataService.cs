namespace HappyMe.Services.Data.Contracts
{
    using HappyMe.Data.Models;
    using HappyMe.Services.Common;

    public interface IQuestionsDataService : IService
    {
        bool IsCorrectAnswer(int questionId, int answerId);
    }
}
