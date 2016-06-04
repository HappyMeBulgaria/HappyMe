namespace HappyMe.Web.InputModels.Feedback
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}