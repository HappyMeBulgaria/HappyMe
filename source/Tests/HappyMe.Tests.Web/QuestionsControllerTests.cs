namespace HappyMe.Tests.Web
{
    using HappyMe.Services.Common.Mapping;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Controllers;

    using Moq;

    public class QuestionsControllerTests : BaseWebTests
    {
        private readonly QuestionsController questionsController;
        private readonly IQuestionsDataService questionsDataService;
        private readonly IMappingService mappingService;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly IAnswersDataService answersDataService;

        public QuestionsControllerTests()
        {
            this.questionsDataService = new Mock<IQuestionsDataService>().Object;

            this.moduleSessionDataService = new Mock<IModuleSessionDataService>().Object;

            this.answersDataService = new Mock<IAnswersDataService>().Object;

            this.mappingService = new AutoMapperMappingService();

            this.questionsController = new QuestionsController(
                this.moduleSessionDataService, 
                this.answersDataService, 
                this.questionsDataService, 
                this.mappingService);
        }
    }
}
