namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using HappyMe.Common.Constants;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Controllers.Base;
    
    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class AdministrationController : BaseAuthorizationController
    {
        public AdministrationController(IUsersDataService userData) 
            : base(userData)
        {
        }
    }
}