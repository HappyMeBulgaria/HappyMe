namespace HappyMe.Services.Data.Contracts
{
    public interface IQuestionsDataService
    {
        bool IsCorrectAnswer(int questionId, int answerId);
    }
}
