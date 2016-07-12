namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;

    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class AdministrationController : BaseAuthorizationController
    {
        public AdministrationController(IUsersDataService userData)
            : base(userData)
        {
        }

        protected ActionResult ItemNotFound(string message)
        {
            this.TempData.AddDangerMessage(message);
            return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
        }
    }
}