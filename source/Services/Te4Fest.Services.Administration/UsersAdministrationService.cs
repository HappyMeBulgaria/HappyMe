namespace Te4Fest.Services.Administration
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Base;
    using Te4Fest.Services.Administration.Contracts;

    public class UsersAdministrationService : AdministrationService<User>, IUsersAdministrationService
    {
        public UsersAdministrationService(IRepository<User> entities)
            : base(entities)
        {
        }

        public User GetByUsername(string username) => this.Entities.All().FirstOrDefault(e => e.UserName == username);
    }
}
