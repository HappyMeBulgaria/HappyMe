namespace HappyMe.Services.Data.Contracts
{
    using HappyMe.Data.Models;

    public interface IQuestionsDataService
    {
        bool IsCorrectAnswer(int questionId, int answerId);
    }
}
