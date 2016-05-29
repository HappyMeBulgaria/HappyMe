namespace HappyMe.Services.Administration.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IUsersInRolesAdministrationService : IAdministrationService<IdentityUserRole>
    {
        void Create(string userId, string roleId);
    }
}
