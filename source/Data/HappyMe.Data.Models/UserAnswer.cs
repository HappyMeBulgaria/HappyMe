namespace HappyMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class UserAnswer : IEntity, IIdentifiable<int>
    {
        public UserAnswer(int answerId, int moduleInstanceId)
            : this(null, answerId, moduleInstanceId)
        {
        }

        public UserAnswer(string userId, int answerId, int moduleInstanceId)
        {
            this.UserId = userId;
            this.AnswerId = answerId;
            this.ModuleInstanceId = moduleInstanceId;
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public int ModuleInstanceId { get; set; }

        public virtual ModuleSession ModuleSession { get; set; }
    }
}
