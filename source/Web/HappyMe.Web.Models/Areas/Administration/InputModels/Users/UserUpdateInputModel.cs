namespace HappyMe.Web.Models.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Attributes;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class UserUpdateInputModel : IMapFrom<User>, IMapTo<User>
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [PlaceHolder("Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [PlaceHolder("Имейл")]
        public string Email { get; set; }
    }
}