namespace HappyMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Data.Contracts;
    using HappyMe.Data.Contracts.Models;

    public class Image : DeletableEntity, IIdentifiable<int>
    {
        private ICollection<Question> questions;
        private ICollection<Answer> answers;
        private ICollection<User> users; 
        private ICollection<Module> modules; 

        public Image()
        {
            this.questions = new HashSet<Question>();
            this.answers = new HashSet<Answer>();
            this.users = new HashSet<User>();
            this.modules = new HashSet<Module>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Path { get; set; }

        public byte[] ImageData { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        public virtual ICollection<Module> Modules
        {
            get { return this.modules; }
            set { this.modules = value; }
        } 
    }
}