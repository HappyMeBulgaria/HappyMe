namespace HappyMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class UserAnswer : IEntity
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AnswerId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int ModuleInstanceId { get; set; }

        public virtual User User { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual ModuleSession ModuleSession { get; set; }
    }
}
