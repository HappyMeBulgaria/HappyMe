namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class AdministrationController : BaseAuthorizationController
    {
        public AdministrationController(IUsersDataService userData, UserManager<User> userManager)
            : base(userData, userManager)
        {
        }

        protected IActionResult ItemNotFound(string message)
        {
            this.TempData.AddDangerMessage(message);
            return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
        }
    }
}
