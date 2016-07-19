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

    public class AnswersDataServiceTests
    {
        private readonly AnswersDataService answersDataService;
        private readonly IRepository<UserAnswer> userAnswerRepositoryMock;
        private readonly IRepository<Answer> answerRepositoryMock;


        public AnswersDataServiceTests()
        {
            this.userAnswerRepositoryMock = new InMemoryRepository<UserAnswer, int>();
            this.answerRepositoryMock = new InMemoryRepository<Answer, int>();

            this.answersDataService = new AnswersDataService(
                this.userAnswerRepositoryMock,
                this.answerRepositoryMock);
        }

        [Fact]
        public async Task AnswerAsUser_ShouldCreateANewUserAnswer()
        {
            var fakeUserId = "XFAKEUSER";
            var fakeAnswerId = 12;
            var fakeModuleSessionId = 12;

            await this.answersDataService.AnswerAsUser(
                fakeUserId, 
                fakeAnswerId, 
                fakeModuleSessionId);

            Assert.Equal(this.userAnswerRepositoryMock.All().Count(), 1);

            var userAnswerExists =
                this.userAnswerRepositoryMock.All()
                    .Any(
                        x => x.AnswerId == fakeAnswerId
                        && x.UserId == fakeUserId
                        && x.ModuleSessionId == fakeModuleSessionId);

            Assert.True(userAnswerExists);
        }

        [Fact]
        public async Task AnswerAsUser_SholdNotAcceptNullUserId()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                 this.answersDataService.AnswerAsUser(null, 12, 12));
        }

        [Fact]
        public async Task AnswerAsAnonymous_ShouldCreateANewAnonymousAnswer()
        {
            var fakeAnswerId = 12;
            var fakeModuleSessionId = 12;

            await this.answersDataService.AnswerAsAnonymous(
                fakeAnswerId, 
                fakeModuleSessionId);

            Assert.Equal(this.userAnswerRepositoryMock.All().Count(), 1);

            var answerExists =
                this.userAnswerRepositoryMock.All()
                    .Any(
                        x => x.AnswerId == fakeAnswerId
                        && x.UserId == null
                        && x.ModuleSessionId == fakeModuleSessionId);

            Assert.True(answerExists);
        }
    }
}
