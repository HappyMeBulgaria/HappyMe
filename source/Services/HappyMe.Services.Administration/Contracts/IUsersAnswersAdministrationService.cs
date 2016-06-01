namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using HappyMe.Data.Models;

    public interface IUsersAnswersAdministrationService : IAdministrationService<UserAnswer>
    {
        IQueryable<UserAnswer> GetChildrenAnswers(string userId);

        bool CheckIfUserHasRights(string userId, params object[] id);
    }
}
