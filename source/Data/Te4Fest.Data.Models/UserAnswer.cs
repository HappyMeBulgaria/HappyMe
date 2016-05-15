namespace Te4Fest.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserAnswer
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AnswerId { get; set; }

        public virtual User User { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
