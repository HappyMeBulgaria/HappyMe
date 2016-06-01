namespace HappyMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Data.Contracts.Models;

    public class ModuleSession : AuditInfo
    {
        private ICollection<UserAnswer> usersAnswers;

        public ModuleSession()
        {
            this.usersAnswers = new HashSet<UserAnswer>();
        }    

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual string UserId { get; set; }

        public User User { get; set; }

        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual ICollection<UserAnswer> UsersAnswers
        {
            get { return this.usersAnswers; }
            set { this.usersAnswers = value; }
        }
    }
}
