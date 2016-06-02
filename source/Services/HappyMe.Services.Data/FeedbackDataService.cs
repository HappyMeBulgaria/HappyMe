namespace HappyMe.Services.Data
{
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class FeedbackDataService : IFeedbackDataService
    {
        private readonly IRepository<Feedback> feedbackRepository;

        public FeedbackDataService(IRepository<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public Task Add(string name, string email, string subject, string message)
        {
            var feedback = new Feedback { Email = email, Name = name, Title = subject, Message = message };
            this.feedbackRepository.Add(feedback);
            return this.feedbackRepository.SaveChangesAsync();
        }
    }
}
