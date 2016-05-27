namespace Te4Fest.Services.Administration
{
    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Base;
    using Te4Fest.Services.Administration.Contracts;

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
