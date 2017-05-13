namespace HappyMe.Web.InputModels.Feedback
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackInputModel
    {
        ////[Required(
        ////    ErrorMessageResourceName = "Required_field_error_generic",
        ////    ErrorMessageResourceType = typeof(GlobalCommonResource))]
        ////[EmailAddress(
        ////    ErrorMessageResourceName = "Invalid_email_address_error_generic",
        ////    ErrorMessageResourceType = typeof(GlobalCommonResource))]
        public string Email { get; set; }

        ////[Required(
        ////    ErrorMessageResourceName = "Required_field_error_generic",
        ////    ErrorMessageResourceType = typeof(GlobalCommonResource))]
        public string Name { get; set; }

        ////[Required(
        ////    ErrorMessageResourceName = "Required_field_error_generic",
        ////    ErrorMessageResourceType = typeof(GlobalCommonResource))]
        public string Subject { get; set; }

        ////[Required(
        ////    ErrorMessageResourceName = "Required_field_error_generic",
        ////    ErrorMessageResourceType = typeof(GlobalCommonResource))]
        public string Message { get; set; }
    }
}
