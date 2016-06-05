namespace HappyMe.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;
    using HappyMe.Web.Common.Attributes;

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