namespace Te4Fest.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Te4Fest.Common.Constants;
    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Contracts.Models;

    public class User : IdentityUser, IDeletableEntity, IAuditInfo, IIdentifiable<string>
    {
        public User()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Required]
        [EmailAddress]
        [MinLength(UserValidationConstants.EmailMinLength)]
        [MaxLength(UserValidationConstants.EmailMaxLength)]
        public override string Email { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
