namespace Te4Fest.Web.Controllers.Base
{
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Data.Contracts;

    public class BaseAuthorizationController : BaseController
    {
        public BaseAuthorizationController(IUsersDataService userData)
        {
            this.UsersData = userData;
            this.SetCurrentUser();
        }

        protected IUsersDataService UsersData { get; set; }

        protected User UserProfile { get; set; }

        private void SetCurrentUser()
        {
            var username = this.User.Identity.Name;
            if (username != null)
            {
                this.UserProfile = this.UsersData.GetUserByUsername(username);
            }
        }
    }
}