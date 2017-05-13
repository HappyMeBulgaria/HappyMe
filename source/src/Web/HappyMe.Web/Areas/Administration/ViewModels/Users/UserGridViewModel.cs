namespace HappyMe.Web.Areas.Administration.ViewModels.Users
{
    using System;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class UserGridViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
