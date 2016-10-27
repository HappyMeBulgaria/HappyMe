namespace HappyMe.Web.Models.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    using GlobalCommonResource = Resources.GlobalCommon;
    using Resource = Resources.Manage.ViewModels.AddPhoneNumberViewModel;

    public class AddPhoneNumberViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [Phone(
            ErrorMessageResourceName = "Phone_number_invalid_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [Display(ResourceType = typeof(Resource), Name = "Phone_number")]
        public string Number { get; set; }
    }
}