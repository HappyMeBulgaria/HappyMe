namespace Te4Fest.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Contracts.Models;

    public class Image : DeletableEntity, IIdentifiable<int>, IEntity
    {
        private ICollection<Question> questions;
        private ICollection<Answer> answers;

        public Image()
        {
            this.questions = new HashSet<Question>();
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

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
    }
}
