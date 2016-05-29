namespace HappyMe.Web.Areas.Administration.ViewModels.Dashboard
{
    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ChildViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}