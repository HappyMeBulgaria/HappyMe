namespace HappyMe.Tests.Web
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Controllers;
    using HappyMe.Web.InputModels.Feedback;

    using MoreDotNet.Extentions.Common;

    using Xunit;

    public class FeedbackControllerTests : BaseWebTests
    {
        private readonly FeedbackController feedbackController;
        private readonly IFeedbackDataService feedbackDataService;
        private readonly IRepository<Feedback> feedbackRepository;

        public FeedbackControllerTests()
        {
            this.feedbackRepository = new InMemoryRepository<Feedback, int>();
            this.feedbackDataService = new FeedbackDataService(this.feedbackRepository);
            this.feedbackController = new FeedbackController(this.feedbackDataService);
        }

        [Fact]
        public void Success_ShouldReturnEmptyView()
        {
            var result = this.feedbackController.Success();
            Assert.IsType<ViewResult>(result);
            var castResult = result.As<ViewResult>();
            Assert.Null(castResult.ViewData.Model);
        }

        [Fact]
        public async Task Send_ShouldRedirectToHomeIfModelIsNull()
        {
            var result = await this.feedbackController.Send(null);
            Assert.IsType<RedirectToRouteResult>(result);

            var castResult = result.As<RedirectToRouteResult>();
            Assert.Equal(1, this.feedbackController.TempData.Keys.Count);
            Assert.True(this.feedbackController.TempData.ContainsKey(GlobalConstants.WariningMessage));
            Assert.Equal("Невалидна обратна връзка", this.feedbackController.TempData[GlobalConstants.WariningMessage]);

            Assert.Equal("Index", castResult.RouteValues["Action"]);
        }

        [Fact]
        public async Task Send_ShouldRedirectToHomeIfModelStateIsInvalid()
        {
            var fakeInputModel = new FeedbackInputModel();
            this.TryValidateModel(fakeInputModel, this.feedbackController);

            var result = await this.feedbackController.Send(fakeInputModel);

            Assert.IsType<RedirectToRouteResult>(result);

            var castResult = result.As<RedirectToRouteResult>();
            Assert.Equal(1, this.feedbackController.TempData.Keys.Count);
            Assert.True(this.feedbackController.TempData.ContainsKey(GlobalConstants.WariningMessage));
            Assert.Equal("Невалидна обратна връзка", this.feedbackController.TempData[GlobalConstants.WariningMessage]);

            Assert.Equal("Index", castResult.RouteValues["Action"]);
        }

        [Fact]
        public async Task Send_ShouldRedirectToSuccessIfInputModelIsValid()
        {
            var fakeInputModel = new FeedbackInputModel()
            {
                Name = "john doe",
                Email = "test@me.me",
                Subject = "Sample subject",
                Message = "Some interesting message here"
            };

            this.TryValidateModel(fakeInputModel, this.feedbackController);

            var result = await this.feedbackController.Send(fakeInputModel);

            Assert.IsType<RedirectToRouteResult>(result);

            var castResult = result.As<RedirectToRouteResult>();
            Assert.Equal("Success", castResult.RouteValues["Action"]);
        }
    }
}
