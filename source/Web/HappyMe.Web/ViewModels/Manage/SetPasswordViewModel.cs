namespace HappyMe.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;

    using GlobalCommonResource = Resources.GlobalCommon;
    using Resource = Resources.Manage.ViewModels.SetPasswordViewModel;

    public class SetPasswordViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [StringLength(
            UserValidationConstants.PasswordMaxLength, 
            ErrorMessageResourceName = "Length_error_generic", 
            MinimumLength = UserValidationConstants.PasswordMinLength,
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [DataType(DataType.Password)]
        [Display(
            ResourceType = typeof(Resource), 
            Name = "New_password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(
            ResourceType = typeof(Resource), 
            Name = "Confirm_new_password")]
        [Compare(
            "NewPassword", 
            ErrorMessageResourceType = typeof(Resource), 
            ErrorMessageResourceName = "Password_do_not_match")]
        public string ConfirmPassword { get; set; }
    }
}