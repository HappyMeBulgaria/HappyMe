namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;

    using GlobalCommonResource = Resources.GlobalCommon;
    using ResourceCommon = Resources.Account.AccountCommon;

    public class ResetPasswordViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(ResourceCommon))]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [StringLength(
            UserValidationConstants.PasswordMaxLength, 
            ErrorMessageResourceName = "Length_error_generic", 
            MinimumLength = UserValidationConstants.PasswordMinLength,
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ResourceCommon))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm_password", ResourceType = typeof(ResourceCommon))]
        [Compare(
            "Password",
            ErrorMessageResourceName = "Confirm_password_no_match_error",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}