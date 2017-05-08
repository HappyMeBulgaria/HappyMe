﻿namespace HappyMe.Services.Administration
{
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class UsersInRolesAdministrationService : 
        AdministrationService<IdentityUserRole<string>>, 
        IUsersInRolesAdministrationService
    {
        public UsersInRolesAdministrationService(IRepository<IdentityUserRole<string>> entities)
            : base(entities)
        {
        }

        public void Create(string userId, string roleId)
        {
            var userInRole = new IdentityUserRole<string> { RoleId = roleId, UserId = userId };
            this.Create(userInRole);
        }
    }
}