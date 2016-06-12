namespace HappyMe.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(
            UserValidationConstants.PasswordMaxLength, 
            ErrorMessage = "The {0} must be at least {2} characters long.", 
            MinimumLength = UserValidationConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}