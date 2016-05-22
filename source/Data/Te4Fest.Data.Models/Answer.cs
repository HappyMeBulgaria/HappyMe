﻿namespace Te4Fest.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Contracts.Models;

    public class Answer : DeletableEntity, IIdentifiable<int>, IOrderable
    {
        private ICollection<UserAnswer> answersByUsers;

        public Answer()
        {
            this.answersByUsers = new HashSet<UserAnswer>();
        }

        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
        
        public bool IsHidden { get; set; }

        public int OrderBy { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<UserAnswer> AnswersByUsers
        {
            get { return this.answersByUsers; }
            set { this.answersByUsers = value; }
        }
    }
}
