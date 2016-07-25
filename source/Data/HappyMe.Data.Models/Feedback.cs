namespace HappyMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Contracts.Models;

    public class Feedback : AuditInfo, IIdentifiable<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.FeedbackTitleMinLength)]
        [MaxLength(GlobalConstants.FeedbackTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(UserValidationConstants.EmailMinLength)]
        [MaxLength(UserValidationConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(GlobalConstants.FeedbackNameMinLength)]
        [MaxLength(GlobalConstants.FeedbackNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.FeedbackMessageMinLength)]
        [MaxLength(GlobalConstants.FeedbackMessageMaxLength)]
        public string Message { get; set; }
    }
}
