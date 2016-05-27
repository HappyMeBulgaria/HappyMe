namespace HappyMe.Services.Administration
{
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    public class UsersInRolesAdministrationService : 
        AdministrationService<UserInRole>, 
        IUsersInRolesAdministrationService
    {
        public UsersInRolesAdministrationService(IRepository<UserInRole> entities)
            : base(entities)
        {
        }

        public void Create(string userId, string roleId)
        {
            var userInRole = new UserInRole { RoleId = roleId, UserId = userId };
            this.Create(userInRole);
        }
    }
}
