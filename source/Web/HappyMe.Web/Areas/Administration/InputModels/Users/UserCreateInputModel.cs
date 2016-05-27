namespace HappyMe.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class UserCreateInputModel : IMapFrom<User>, IMapTo<User>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsSamePassword { get; set; }
    }
}