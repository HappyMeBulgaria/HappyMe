namespace HappyMe.Services.Administration
{
    using System.Linq;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    public class UsersAdministrationService : AdministrationService<User>, IUsersAdministrationService
    {
        public UsersAdministrationService(IRepository<User> entities)
            : base(entities)
        {
        }

        public User GetByUsername(string username) => this.Entities.All().FirstOrDefault(e => e.UserName == username);
    }
}
