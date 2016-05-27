namespace HappyMe.Web.Controllers.Base
{
    using System;
    using System.Web.Routing;

    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class BaseAuthorizationController : BaseController
    {
        public BaseAuthorizationController(IUsersDataService userData)
        {
            this.UsersData = userData;
        }

        protected IUsersDataService UsersData { get; set; }

        protected User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var username = requestContext.HttpContext.User.Identity.Name;
            this.SetCurrentUser(username);

            return base.BeginExecute(requestContext, callback, state);
        }

        private void SetCurrentUser(string username)
        {
            if (username != null)
            {
                this.UserProfile = this.UsersData.GetUserByUsername(username);
            }
        }
    }
}