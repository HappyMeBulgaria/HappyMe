namespace Te4Fest.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Contracts.Models;

    public class Module : DeletableEntity, IIdentifiable<int>
    {
        private ICollection<Question> questions;
        private ICollection<UserInModule> usersInModule; 

        public Module()
        {
            this.questions = new HashSet<Question>();  
            this.usersInModule = new HashSet<UserInModule>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        public virtual ICollection<UserInModule> UsersInModule
        {
            get { return this.usersInModule; }
            set { this.usersInModule = value; }
        }
    }
}
