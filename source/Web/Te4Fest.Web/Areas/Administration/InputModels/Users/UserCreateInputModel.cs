namespace Te4Fest.Web.Areas.Administration.InputModels.Users
{
    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class UserCreateInputModel : IMapFrom<User>, IMapTo<User>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsSamePassword { get; set; }
    }
}