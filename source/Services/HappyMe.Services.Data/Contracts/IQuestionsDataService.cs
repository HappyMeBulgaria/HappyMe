namespace HappyMe.Services.Data.Contracts
{
    using HappyMe.Services.Common;

    public interface IQuestionsDataService : IService
    {
        bool IsCorrectAnswer(int questionId, int answerId);
    }
}
