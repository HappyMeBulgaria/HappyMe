namespace HappyMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using GlobalCommonResource = Resources.GlobalCommon;
    using Resource = Resources.Account.ViewModels.VerifyCodeViewModel;
    using ResourceCommon = Resources.Account.AccountCommon;

    public class VerifyCodeViewModel
    {
        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        public string Provider { get; set; }

        [Required(
            ErrorMessageResourceName = "Required_field_error_generic",
            ErrorMessageResourceType = typeof(GlobalCommonResource))]
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember_browser", ResourceType = typeof(Resource))]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}