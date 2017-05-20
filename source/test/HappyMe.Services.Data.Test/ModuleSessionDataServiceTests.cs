namespace HappyMe.Services.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;

    using Moq;

    using Xunit;

    public class ModuleSessionDataServiceTests
    {
        private readonly IRepository<ModuleSession> moduleSessionsRepository;
        private readonly IRepository<UserAnswer> userAnswersRepository;
        private readonly IRepository<Module> modulesRepository;
        private readonly IRepository<Question> questionsRepository;
        private readonly ModuleSessionDataService moduleSessionDataService;

        public ModuleSessionDataServiceTests()
        {
            this.moduleSessionsRepository = new InMemoryRepository<ModuleSession, int>();
            this.userAnswersRepository = new InMemoryRepository<UserAnswer, int>();
            this.modulesRepository = new InMemoryRepository<Module, int>();
            this.questionsRepository = new InMemoryRepository<Question, int>();

            this.moduleSessionDataService = new ModuleSessionDataService(
                this.moduleSessionsRepository,
                this.userAnswersRepository,
                this.modulesRepository,
                this.questionsRepository);

            var simpleModule = new Module { Id = 1 };
            var fakeUser = new User
            {
                Id = "NEWXUSER"
            };

            var simpleModuleSession = new ModuleSession
            {
                Id = 1,
                Module = simpleModule,
                User = fakeUser
            };

            this.modulesRepository.Add(simpleModule);
            this.moduleSessionsRepository.Add(simpleModuleSession);
        }

        [Fact]
        public void GetById_ShouldReturnModuleSessionIfValidIdIsGiven()
        {
            var result = this.moduleSessionDataService.GetById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_ShouldReturnNullIfInvalidIdIsGiven()
        {
            var result = this.moduleSessionDataService.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void FinishSession_ShouldFinishSessionIfValidIdIsGiven()
        {
            var testSession = new ModuleSession { Id = 3 };
            this.moduleSessionsRepository.Add(testSession);
            this.moduleSessionDataService.FinishSession(3);

            Assert.True(testSession.IsFinised);

            // TODO: Mock DateTime.Now
            Assert.NotNull(testSession.FinishDate);
        }

        [Fact]
        public void FinishSession_ShouldThrowExceptionIfInvalidIdIsGiven()
        {
            Assert.Throws<InvalidOperationException>(() => this.moduleSessionDataService.FinishSession(34));
        }

        [Fact]
        public async Task StartAnonymousSession_ShouldStartNewSessionIfValidModuleIdIsGiven()
        {
            var result = await this.moduleSessionDataService.StartAnonymousSession(1);

            Assert.NotNull(result);
            Assert.Equal(result.ModuleId, 1);
            Assert.NotNull(result.StartedDate);
        }

        [Fact]
        public async Task StartAnonymousSession_ShouldThrowExceptionIfInvalidModuleIdIsGiven()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => this.moduleSessionDataService.StartAnonymousSession(13));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("            ")]
        public async Task StartUserSessionn_ShouldThrowExceptionIfInvalidUserIdIsGiven(string userId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.moduleSessionDataService.StartUserSession(userId, 1));
        }

        [Theory]
        [InlineData(99)]
        [InlineData(-99)]
        [InlineData(0)]
        public async Task StartUserSessionn_ShouldThrowExceptionIfInvalidModuleIdIsGiven(int moduleId)
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => this.moduleSessionDataService.StartUserSession("Test", moduleId));
        }

        [Fact]
        public async Task StartUserSessionn_ShouldStartNewSessionIfValidInputIsGiven()
        {
            var result = await this.moduleSessionDataService.StartUserSession("testUser", 1);

            Assert.NotNull(result);
            Assert.Equal(result.ModuleId, 1);
            Assert.NotNull(result.StartedDate);
            Assert.Equal("testUser", result.UserId);
        }

        [Fact]
        public void NextQuestion_ShouldReturnNullIfModuleForSessionHasNoQuestions()
        {
            var session = this.moduleSessionsRepository.GetById(1);
            var result = this.moduleSessionDataService.NextQuestion(1, "NEWXUSER");

            Assert.Null(result);
            Assert.True(session.IsFinised);
        }

        [Fact]
        public void NextQuestion_ShouldReturnNullIfModuleForSessionHasNoUnansweredQuestions()
        {
            var correctAnswer = new Answer { Id = 3, IsCorrect = true, QuestionId = 3 };

            var simpleModule = new Module
            {
                Id = 3,
                QuestionsInModules = new List<QuestionInModule>
                {
                    new QuestionInModule
                    {
                        ModuleId = 3,
                        QuestionId = 3,
                        Question = new Question
                        {
                            Id = 3,
                            Answers = new List<Answer>
                            {
                                correctAnswer
                            }
                        }
                    }
                }
            };

            var fakeUser = new User
            {
                Id = "NEWXUSER"
            };

            var simpleModuleSession = new ModuleSession
            {
                Id = 3,
                Module = simpleModule,
                User = fakeUser
            };

            var userAnswer = new UserAnswer("NEWXUSER", 3, 3)
            {
                Answer = correctAnswer
            };

            this.userAnswersRepository.Add(userAnswer);

            this.moduleSessionsRepository.Add(simpleModuleSession);
            var result = this.moduleSessionDataService.NextQuestion(3, "NEWXUSER");

            Assert.Null(result);
            Assert.True(simpleModuleSession.IsFinised);
        }

        [Fact]
        public void NextQuestion_ShouldReturnNextQuestionForSessionIfThereExistsANonANswerdQuestion()
        {
            var correctAnswer = new Answer { Id = 3, IsCorrect = true, QuestionId = 3 };

            var simpleQuestion = new Question
            {
                Id = 3,
                Answers = new List<Answer>
                {
                    correctAnswer
                }
            };

            var simpleModule = new Module
            {
                Id = 3,
                QuestionsInModules = new List<QuestionInModule>
                {
                    new QuestionInModule
                    {
                        ModuleId = 3,
                        QuestionId = 3,
                        Question = simpleQuestion
                    }
                }
            };

            var fakeUser = new User
            {
                Id = "NEWXUSER"
            };

            var simpleModuleSession = new ModuleSession
            {
                Id = 3,
                Module = simpleModule,
                User = fakeUser
            };

            this.questionsRepository.Add(simpleQuestion);
            this.moduleSessionsRepository.Add(simpleModuleSession);
            var result = this.moduleSessionDataService.NextQuestion(3, "NEWXUSER");

            Assert.NotNull(result);
            Assert.Equal(result.Id, 3);
        }

        // TODO: Test randomization
    }
}
