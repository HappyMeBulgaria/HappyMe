namespace HappyMe.Tests.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;

    using Xunit;

    public class ModulesDataServiceTests
    {
        private readonly IRepository<Module> modulesRepository;
        private readonly ModulesDataService modulesDataService;

        public ModulesDataServiceTests()
        {
            this.modulesRepository = new InMemoryRepository<Module, int>();
            this.modulesDataService = new ModulesDataService(this.modulesRepository);

            var simpleModule = new Module { Id = 1 };
            var moduleWithQuestion = new Module
            {
                Id = 2,
                Questions = new List<Question>
                {
                    new Question { Id = 1 }
                }
            };
            var activeModule = new Module { Id = 3, IsActive = true };
            var activeModuleWithQuestions = new Module
            {
                Id = 4,
                IsActive = true,
                Questions = new List<Question>
                {
                    new Question { Id = 2 }
                }
            };
            var publicModule = new Module { Id = 5, IsPublic = true };
            var publicModuleWithQuestionsWithCorrectAnswer = new Module
            {
                Id = 4,
                IsPublic = true,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 2,
                        Answers = new List<Answer>
                        {
                            new Answer
                            {
                                Id = 1,
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            this.modulesRepository.Add(simpleModule);
            this.modulesRepository.Add(moduleWithQuestion);
            this.modulesRepository.Add(activeModule);
            this.modulesRepository.Add(activeModuleWithQuestions);
            this.modulesRepository.Add(publicModule);
            this.modulesRepository.Add(publicModuleWithQuestionsWithCorrectAnswer);
        }

        [Fact]
        public void All_ShouldReturnAllModules()
        {
            var result = this.modulesDataService.All();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(6, result.Count());
        }

        [Fact]
        public void AllWithQuestions_ShouldReturnAllModulesWithAnyQuestions()
        {
            var result = this.modulesDataService.AllWithQuestions();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.True(result.All(x => x.Questions.Any()));
        }

        [Fact]
        public void AllActive_ShouldReturnAllActiveModules()
        {
            var result = this.modulesDataService.AllActive();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.True(result.All(x => x.IsActive));
        }

        [Fact]
        public void AllActiveWithQuestions_ShouldReturnAllActiveModulesWithAnyQuestions()
        {
            var result = this.modulesDataService.AllActiveWithQuestions();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.True(result.All(x => x.IsActive));
            Assert.True(result.All(x => x.Questions.Any()));
        }

        [Fact]
        public void AllPublic_ShouldReturnAllPublicModules()
        {
            var result = this.modulesDataService.AllPublic();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.True(result.All(x => x.IsPublic));
        }

        [Fact]
        public void AllPublicWithQuestionsWithCorrectAnswer_ShouldReturnAllPublicModulesWithQuestionsContainingCorrectAnswers()
        {
            var result = this.modulesDataService.AllPublicWithQuestionsWithCorrectAnswer();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.True(result.All(x => x.IsPublic));
            Assert.True(result.All(x => x.Questions.Any(y => y.Answers.Any(z => z.IsCorrect))));
        }

        [Fact]
        public void GetById_ShouldReturnModuleIfCorrectIdIsGiven()
        {
            var result = this.modulesDataService.GetById(3);

            Assert.NotNull(result);
            Assert.True(result.IsActive);
        }

        [Fact]
        public void GetById_ShouldReturnNullIfIncorrectIdIsGiven()
        {
            var result = this.modulesDataService.GetById(100);

            Assert.Null(result);
        }
    }
}
