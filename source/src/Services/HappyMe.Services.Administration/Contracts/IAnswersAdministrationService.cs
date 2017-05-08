namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using HappyMe.Data.Models;

    public interface IAnswersAdministrationService : IAdministrationService<Answer>
    {
        IQueryable<Answer> GetAllUserAnswers(string userId);

        bool CheckIfUserIsAnswerAuthor(int answerId, string userId);
    }
}
