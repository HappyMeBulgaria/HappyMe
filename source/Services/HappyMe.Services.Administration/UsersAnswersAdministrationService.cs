namespace HappyMe.Services.Administration
{
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    public class UsersAnswersAdministrationService : 
        AdministrationService<UserAnswer>, 
        IUsersAnswersAdministrationService
    {
        public UsersAnswersAdministrationService(IRepository<UserAnswer> entities) 
            : base(entities)
        {
        }

        public IQueryable<UserAnswer> GetChildrenAnswers(string userId) => 
            this.Read().Where(ua => ua.User.ParentId == userId);
    }
}
