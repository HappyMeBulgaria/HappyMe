namespace HappyMe.Services.Administration.Contracts
{
    using HappyMe.Data.Models;

    public interface IUsersAdministrationService : IAdministrationService<User>
    {
        User GetByUsername(string username);
    }
}
