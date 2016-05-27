namespace Te4Fest.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Te4Fest.Data.Contracts;

    public class Role : IdentityRole, IEntity
    {
    }
}
