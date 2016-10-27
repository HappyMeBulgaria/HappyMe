namespace HappyMe.Web.Models.Areas.Administration.ViewModels.Users
{
    using System;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class UserGridViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}