namespace HappyMe.Tests.Services.Data
{
    using System.Collections.Generic;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;

    using Xunit;

    public class QuestionsDataServiceTests
    {
        private readonly IRepository<Question> questionRepository;
        private readonly QuestionsDataService questionsDataService;

        public QuestionsDataServiceTests()
        {
            this.questionRepository = new InMemoryRepository<Question, int>();

            this.questionsDataService = new QuestionsDataService(this.questionRepository);

            var simpleTrueAnswer = new Answer { Id = 1, IsCorrect = true };
            var simpleFalseAnswer = new Answer { Id = 2 };

            var simpleQuestion = new Question
            {
                Id = 1,
                Answers = new List<Answer>()
                {
                    simpleTrueAnswer,
                    simpleFalseAnswer
                }
            };

            this.questionRepository.Add(simpleQuestion);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-100)]
        [InlineData(0)]
        public void IsCorrectAnswer_ShouldReturnFalseIfInvalidQuestionIdIsGiven(int questionId)
        {
            Assert.False(this.questionsDataService.IsCorrectAnswer(questionId, 1));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-100)]
        [InlineData(0)]
        public void IsCorrectAnswer_ShouldReturnFalseIfInvalidAnswerIdIsGiven(int answerId)
        {
            Assert.False(this.questionsDataService.IsCorrectAnswer(1, answerId));
        }

        [Fact]
        public void IsCorrectAnswer_ShouldReturnFalseIfAnswerIsInCorrect()
        {
            Assert.False(this.questionsDataService.IsCorrectAnswer(1, 2));
        }

        [Fact]
        public void IsCorrectAnswer_ShouldReturnTrueIfAnswerIsCorrect()
        {
            Assert.True(this.questionsDataService.IsCorrectAnswer(1, 1));
        }
    }
}
