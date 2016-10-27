namespace HappyMe.Web.Models.Areas.Administration.ViewModels.Feedback
{
    using Data.Models;

    using HappyMe.Common.Mapping;

    public class FeedbackGridViewModel : IMapFrom<Feedback>
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }
        
        public string Message { get; set; }
    }
}