namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using ResourceCommon = Resources.Account.AccountCommon;

    public class ForgotViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [EmailAddress(
            ErrorMessageResourceName = "Invalid_email_address_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [Display(Name = "Email", ResourceType = typeof(ResourceCommon))]
        public string Email { get; set; }
    }
}