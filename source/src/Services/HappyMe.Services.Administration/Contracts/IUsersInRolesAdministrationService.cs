namespace HappyMe.Services.Administration.Contracts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public interface IUsersInRolesAdministrationService : IAdministrationService<IdentityUserRole<string>>
    {
        void Create(string userId, string roleId);
    }
}
