namespace HappyMe.Services.Administration
{
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class UsersInRolesAdministrationService : 
        AdministrationService<IdentityUserRole>, 
        IUsersInRolesAdministrationService
    {
        public UsersInRolesAdministrationService(IRepository<IdentityUserRole> entities)
            : base(entities)
        {
        }

        public void Create(string userId, string roleId)
        {
            var userInRole = new IdentityUserRole { RoleId = roleId, UserId = userId };
            this.Create(userInRole);
        }
    }
}
