namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(
            UserValidationConstants.PasswordMaxLength, 
            ErrorMessage = "The {0} must be at least {2} characters long.", 
            MinimumLength = UserValidationConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(
            "Password", 
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}