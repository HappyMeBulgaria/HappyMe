namespace Te4Fest.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
        private ICollection<User> children;
        private ICollection<UserInModule> userInModules;
        private ICollection<UserAnswer> userAnswers; 

        public User()
        {
            this.children = new HashSet<User>();
            this.userInModules = new HashSet<UserInModule>();
            this.userAnswers = new HashSet<UserAnswer>();
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

        public string ParentId { get; set; }

        public virtual User Parent { get; set; }

        public virtual ICollection<User> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public virtual ICollection<UserInModule> UserInModules
        {
            get { return this.userInModules; }
            set { this.userInModules = value; }
        }

        public virtual ICollection<UserAnswer> UserAnswers
        {
            get { return this.userAnswers; }
            set { this.userAnswers = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
