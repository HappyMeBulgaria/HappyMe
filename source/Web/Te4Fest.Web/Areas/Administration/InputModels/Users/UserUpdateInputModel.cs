namespace Te4Fest.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

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