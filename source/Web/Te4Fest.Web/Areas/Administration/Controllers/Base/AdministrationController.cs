namespace Te4Fest.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Te4Fest.Web.Controllers.Base;

    // TODO: Add Authorization filter
    public class AdministrationController : BaseAuthorizationController
    {
        public AdministrationController(IUsersDataService userData) 
            : base(userData)
        {
        }
    }
}