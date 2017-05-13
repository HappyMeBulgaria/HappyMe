namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using Data.Models;

    public interface IQuestionsAdministrationService : IAdministrationService<Question>
    {
        IQueryable<Question> GetUserQuestions(string userId);

        IQueryable<Question> GetUserAndPublicQuestions(string userId);

        bool CheckIfUserIsAuthorOnQuestion(string userId, int questionId);
    }
}
