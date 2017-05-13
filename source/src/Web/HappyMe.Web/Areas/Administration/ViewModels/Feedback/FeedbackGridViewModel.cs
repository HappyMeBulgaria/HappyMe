namespace HappyMe.Web.Areas.Administration.ViewModels.Feedback
{
    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class FeedbackGridViewModel : IMapFrom<Feedback>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }
    }
}
