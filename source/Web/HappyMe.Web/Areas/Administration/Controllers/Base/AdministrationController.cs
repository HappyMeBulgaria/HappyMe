namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Controllers.Base;

    // TODO: Add Authorization filter
    public class AdministrationController : BaseAuthorizationController
    {
        public AdministrationController(IUsersDataService userData) 
            : base(userData)
        {
        }
    }
}