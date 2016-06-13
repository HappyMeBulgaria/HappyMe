namespace HappyMe.Common.Constants
{
    public class GlobalConstants
    {
        public const string InfoMessage = "InfoMessage";
        public const string SuccessMessage = "SuccessMessage";
        public const string WariningMessage = "WariningMessage";
        public const string DangerMessage = "DangerMessage";

        public const int DefaultAccountLockoutTimeInMinutes = 5;
        public const int MaxFailedAccessAttemptsBeforeLockout = 5;

        public const int DefaultPageSize = 10;
        public const string DefaultUserImagePath = "/Content/images/user-default-image.png";

        public const int AnswerTextMaxLength = 100;

        public const int FeedbackTitleMaxLength = 250;
        public const int FeedbackTitleMinLength = 5;

        public const int FeedbackNameMaxLength = 100;
        public const int FeedbackNameMinLength = 3;

        public const int FeedbackMessageMaxLength = 5000;
        public const int FeedbackMessageMinLength = 12;

        public const int ImagePathMaxLength = 500;

        public const int ModuleNameMaxLength = 100;
        public const int ModuleNameMinLength = 5;

        public const int ModuleDescriptionMaxLength = 5000;
        public const int ModuleDescriptionMinLength = 10;

        public const int QuestionTextMaxLength = 100;
        public const int QuestionTextMinLength = 2;
    }
}
