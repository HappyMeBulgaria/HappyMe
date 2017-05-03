namespace HappyMe.Web.Common.Attributes
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using HappyMe.Web.Common.Extensions;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] allRoles;

        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.allRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var user = httpContext.User;
            if (!user.IsLoggedIn() || !this.allRoles.Any(x => user.IsInRole(x)))
            {
                return false;
            }

            return true;
        }
    }
}
