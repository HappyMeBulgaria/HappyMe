namespace Te4Fest.Web.Areas.Administration.ViewModels.Users
{
    using System;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class UserGridViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}