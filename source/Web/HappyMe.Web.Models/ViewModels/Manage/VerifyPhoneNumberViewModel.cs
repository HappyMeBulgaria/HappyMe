namespace HappyMe.Web.Models.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    using Resource = Resources.Manage.ViewModels.VerifyPhoneNumberViewModel;

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(
            ResourceType = typeof(Resource), 
            Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(
            ResourceType = typeof(Resource), 
            Name = "Phone_number")]
        public string PhoneNumber { get; set; }
    }
}