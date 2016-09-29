namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;

    using ResourceCommon = Resources.Account.AccountCommon;

    public class RegisterViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [Display(Name = "First_name", ResourceType = typeof(ResourceCommon))]
        public string FirstName { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [Display(Name = "Last_name", ResourceType = typeof(ResourceCommon))]
        public string LastName { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [Display(Name = "Username", ResourceType = typeof(ResourceCommon))]
        public string Username { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [EmailAddress(
            ErrorMessageResourceName = "Invalid_email_address_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [Display(Name = "Email", ResourceType = typeof(ResourceCommon))]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(ResourceCommon))]
        [StringLength(
            UserValidationConstants.PasswordMaxLength, 
            ErrorMessageResourceName = "Length_error_generic", 
            MinimumLength = UserValidationConstants.PasswordMinLength,
            ErrorMessageResourceType = typeof(ResourceCommon))]
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
    }
}