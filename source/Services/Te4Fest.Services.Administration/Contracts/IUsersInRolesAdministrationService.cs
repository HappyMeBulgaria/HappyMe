namespace Te4Fest.Services.Administration.Contracts
{
    using Te4Fest.Data.Models;

    public interface IUsersInRolesAdministrationService : IAdministrationService<UserInRole>
    {
        void Create(string userId, string roleId);
    }
}
