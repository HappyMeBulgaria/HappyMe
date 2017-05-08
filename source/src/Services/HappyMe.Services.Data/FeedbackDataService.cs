namespace HappyMe.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class FeedbackDataService : IFeedbackDataService
    {
        private readonly IRepository<Feedback> feedbackRepository;

        public FeedbackDataService(IRepository<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        // TODO: Rename title to subject or vise versa for consistency
        public Task Add(string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Feedback must have a name.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Feedback must have a email.");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(nameof(subject), "Feedback must have a subject.");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Feedback must have a message.");
            }

            var feedback = new Feedback { Email = email, Name = name, Title = subject, Message = message };
            this.feedbackRepository.Add(feedback);
            return this.feedbackRepository.SaveChangesAsync();
        }
    }
}
