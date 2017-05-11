namespace HappyMe.Web.Controllers.Base
{
    using System;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    [Authorize]
    public class BaseAuthorizationController : BaseController
    {
        public BaseAuthorizationController(
            IUsersDataService userData,
            UserManager<User> userManager)
            : base(userManager)
        {
            this.UsersData = userData;
        }

        protected IUsersDataService UsersData { get; set; }

        ////protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        ////{
        ////    var username = requestContext.HttpContext.User.Identity.Name;
        ////    this.SetCurrentUser(username);

        ////    return base.BeginExecute(requestContext, callback, state);
        ////}
    }
}