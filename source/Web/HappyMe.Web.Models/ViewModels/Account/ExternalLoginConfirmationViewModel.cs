namespace HappyMe.Web.Models.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using GlobalCommonResource = Resources.GlobalCommon;
    using ResourceCommon = Resources.Account.AccountCommon;

    public class ExternalLoginConfirmationViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic", 
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [Display(
            Name = "Email",
            ResourceType = typeof(ResourceCommon))]
        public string Email { get; set; }
    }
}
