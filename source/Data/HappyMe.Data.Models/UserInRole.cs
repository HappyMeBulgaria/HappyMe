namespace HappyMe.Data.Models
{
    using HappyMe.Data.Contracts;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserInRole : IdentityUserRole, IEntity
    {
    }
}
