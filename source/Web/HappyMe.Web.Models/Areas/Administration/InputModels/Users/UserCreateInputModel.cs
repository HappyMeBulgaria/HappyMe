namespace HappyMe.Web.Models.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Common.Attributes;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class UserCreateInputModel : IMapFrom<User>, IMapTo<User>
    {
        [Required]
        [PlaceHolder("Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [PlaceHolder("Имейл")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PlaceHolder("Парола")]
        public string Password { get; set; }
    }
}