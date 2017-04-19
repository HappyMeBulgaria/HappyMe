namespace HappyMe.Tests.Web.Administration
{
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Areas.Administration.Controllers;

    using Moq;

    using MoreDotNet.Extensions.Common;

    using Xunit;

    public class QuestionsControllerTests : BaseWebTests
    {
        private readonly QuestionsController questionsController;

        public QuestionsControllerTests()
        {
            var userService = new Mock<IUsersDataService>().Object;
            var questionAdminService = new Mock<IQuestionsAdministrationService>().Object;
            var modulesAdministrationService = new Mock<IModulesAdministrationService>().Object;
            var imageAdministrationService = new Mock<IImagesAdministrationService>().Object;

            this.questionsController = new QuestionsController(
                userService,
                questionAdminService,
                new AutoMapperMappingService(),
                modulesAdministrationService,
                imageAdministrationService);
        }

        [Fact]
        public void Update_ShouldRedirectToDashboardIfInvalidQuestionIdIsGiven()
        {
            var result = this.questionsController.Update((int?)null);
            Assert.NotNull(result);

            var redirectResult = result.As<RedirectToRouteResult>();

            Assert.Equal(redirectResult.RouteValues["action"], "Index");
            Assert.Equal(redirectResult.RouteValues["controller"], "Dashboard");
            Assert.Equal(redirectResult.RouteValues["area"], "Administration");

            Assert.True(this.questionsController.TempData.ContainsKey(GlobalConstants.DangerMessage));
        }
    }
}
