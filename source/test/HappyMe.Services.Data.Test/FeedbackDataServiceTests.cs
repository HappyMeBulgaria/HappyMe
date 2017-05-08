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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("            ")]
        public async Task Add_ShouldThrowExceptionIfInvalidNameIsGiven(string name)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add(name, "Test", "Test", "Test"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("            ")]
        public async Task Add_ShouldThrowExceptionIfInvalidEmailIsGiven(string email)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", email, "Test", "Test"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("            ")]
        public async Task Add_ShouldThrowExceptionIfInvalidSubjectIsGiven(string subject)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", subject, "Test"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("            ")]
        public async Task Add_ShouldThrowExceptionIfInvalidMessageIsGiven(string message)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.feedbackDataService.Add("Test", "Test", "Test", message));
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
