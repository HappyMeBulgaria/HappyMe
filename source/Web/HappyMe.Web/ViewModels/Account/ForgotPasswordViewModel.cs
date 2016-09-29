namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using GlobalCommonResource = Resources.GlobalCommon;
    using ResourceCommon = Resources.Account.AccountCommon;

    public class ForgotPasswordViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [EmailAddress(
            ErrorMessageResourceName = "Invalid_email_address_error_generic", 
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [Display(Name = "Email", ResourceType = typeof(ResourceCommon))]
        public string Email { get; set; }
    }
}