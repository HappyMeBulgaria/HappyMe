namespace HappyMe.Data.Models
{
    public class QuestionInModule
    {
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }
    }
}
