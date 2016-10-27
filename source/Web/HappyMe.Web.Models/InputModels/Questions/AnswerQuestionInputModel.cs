namespace HappyMe.Web.Models.InputModels.Questions
{
    public class AnswerQuestionInputModel
    {
        public int SessionId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
    }
}