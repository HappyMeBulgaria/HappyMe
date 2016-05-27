namespace Te4Fest.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Te4Fest.Common.Models;
    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Contracts.Models;

    public class Question : DeletableEntity, IIdentifiable<int>
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10), MaxLength(100)]
        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public bool IsPublic { get; set; }

        public int? ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}