using Microsoft.AspNetCore.Authorization;

namespace HappyMe.Web.Common.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}
