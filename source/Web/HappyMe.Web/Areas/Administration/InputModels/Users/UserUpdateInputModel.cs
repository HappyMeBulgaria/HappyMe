namespace HappyMe.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class UserUpdateInputModel : IMapFrom<User>, IMapTo<User>
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}