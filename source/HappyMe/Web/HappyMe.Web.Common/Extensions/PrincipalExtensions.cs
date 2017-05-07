namespace HappyMe.Web.Common.Extensions
{
    using System.Security.Principal;

    using HappyMe.Common.Constants;

    public static class PrincipalExtensions
    {
        public static bool IsLoggedIn(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(RoleConstants.Administrator);
        }

        public static bool IsParent(this IPrincipal principal)
        {
            return principal.IsInRole(RoleConstants.Parent);
        }

        public static bool IsChild(this IPrincipal principal)
        {
            return principal.IsInRole(RoleConstants.Child);
        }
    }
}
