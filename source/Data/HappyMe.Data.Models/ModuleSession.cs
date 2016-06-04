namespace HappyMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Data.Contracts.Models;

    public class ModuleSession : AuditInfo
    {
        private ICollection<UserAnswer> usersAnswers;

        public ModuleSession(int moduleId)
            : this()
        {
            this.ModuleId = moduleId;
        }

        public ModuleSession(string userId, int moduleId)
            : this(moduleId)
        {
            this.UserId = userId;
        }

        protected ModuleSession()
        {
            this.usersAnswers = new HashSet<UserAnswer>();
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public bool IsFinised { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime FinishDate { get; set; }

        public virtual User User { get; set; }

        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual ICollection<UserAnswer> UsersAnswers
        {
            get { return this.usersAnswers; }
            set { this.usersAnswers = value; }
        }
    }
}
