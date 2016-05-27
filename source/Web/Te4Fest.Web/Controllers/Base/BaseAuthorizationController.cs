namespace Te4Fest.Web.Controllers.Base
{
    using System;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Data.Contracts;

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
            this.SetCurrentUser();

            return base.BeginExecute(requestContext, callback, state);
        }

        private void SetCurrentUser()
        {
            ////var username = this.User.Identity.GetUserName();    
            ////if (username != null)
            ////{
            ////    this.UserProfile = this.UsersData.GetUserByUsername(username);
            ////}
        }
    }
}