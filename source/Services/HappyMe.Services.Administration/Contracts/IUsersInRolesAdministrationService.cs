namespace HappyMe.Services.Administration.Contracts
{
    using HappyMe.Data.Models;

    public interface IUsersInRolesAdministrationService : IAdministrationService<UserInRole>
    {
        void Create(string userId, string roleId);
    }
}
