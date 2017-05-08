using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HappyMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Contracts.Models;

    public class User : IdentityUser, IDeletableEntity, IAuditInfo, IIdentifiable<string>
    {
        private ICollection<User> children;
        private ICollection<UserInModule> userInModules;
        private ICollection<UserAnswer> userAnswers;
        private ICollection<Question> questions;

        public User()
        {
            this.children = new HashSet<User>();
            this.userInModules = new HashSet<UserInModule>();
            this.userAnswers = new HashSet<UserAnswer>();
            this.questions = new HashSet<Question>();
            this.CreatedOn = DateTime.Now;
        }

        [Required]
        [EmailAddress]
        [MinLength(UserValidationConstants.EmailMinLength)]
        [MaxLength(UserValidationConstants.EmailMaxLength)]
        public override string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ParentId { get; set; }

        public virtual User Parent { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

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

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.(this, DefaultAuthenticationTypes.ApplicationCookie);

        //    // Add custom user claims here
        //    return userIdentity;
        //}

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}
