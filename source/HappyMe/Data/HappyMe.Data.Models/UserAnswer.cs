namespace HappyMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class UserAnswer : IEntity, IIdentifiable<int>
    {
        public UserAnswer()
        {
        }

        public UserAnswer(int answerId, int moduleInstanceId)
            : this(null, answerId, moduleInstanceId)
        {
        }

        public UserAnswer(string userId, int answerId, int moduleSessionId)
        {
            this.UserId = userId;
            this.AnswerId = answerId;
            this.ModuleSessionId = moduleSessionId;
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public int ModuleSessionId { get; set; }

        public virtual ModuleSession ModuleSession { get; set; }
    }
}
