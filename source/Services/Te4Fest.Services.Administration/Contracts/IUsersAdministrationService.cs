namespace Te4Fest.Services.Administration.Contracts
{
    using Te4Fest.Data.Models;

    public interface IUsersAdministrationService : IAdministrationService<User>
    {
        User GetByUsername(string username);
    }
}
