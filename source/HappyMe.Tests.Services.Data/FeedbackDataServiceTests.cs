namespace HappyMe.Tests.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;

    using Xunit;

    public class FeedbackDataServiceTests
    {
        private readonly IRepository<Feedback> feedbackRepository;
        private readonly FeedbackDataService feedbackDataService;

        public FeedbackDataServiceTests()
        {
            this.feedbackRepository = new InMemoryRepository<Feedback, int>();
            this.feedbackDataService = new FeedbackDataService(this.feedbackRepository);
        }

        [Fact]
        public async Task Add_ShouldThrowExceptionIfInvalidNameIsGiven()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add(null, "Test", "Test", "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add(string.Empty, "Test", "Test", "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add(new string(' ', 20), "Test", "Test", "Test"));
        }

        [Fact]
        public async Task Add_ShouldThrowExceptionIfInvalidEmailIsGiven()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", null, "Test", "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", string.Empty, "Test", "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", new string(' ', 20), "Test", "Test"));
        }

        [Fact]
        public async Task Add_ShouldThrowExceptionIfInvalidSubjectIsGiven()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", null, "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", string.Empty, "Test"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", new string(' ', 20), "Test"));
        }

        [Fact]
        public async Task Add_ShouldThrowExceptionIfInvalidMessageIsGiven()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", "Test", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", "Test", string.Empty));
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", "Test", new string(' ', 20)));
        }

        [Fact]
        public async Task Add_ShouldAddNewFeedbackIfValidArgumentsAreGiven()
        {
            var name = "TestName";
            var email = "fake@me.com";
            var subject = "Sample subject";
            var message = "Sample message";

            await this.feedbackDataService.Add(name, email, subject, message);

            var databaseEntites =
                this.feedbackRepository
                    .All()
                    .Where(x => 
                        x.Name == name && 
                        x.Email == email && 
                        x.Title == subject && 
                        x.Message == message);

            Assert.NotNull(databaseEntites);
            Assert.NotEmpty(databaseEntites);
            Assert.Equal(1, databaseEntites.Count());
        }
    }
}
