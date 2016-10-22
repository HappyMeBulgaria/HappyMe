namespace HappyMe.Tests.Web
{
    using System.Web.Mvc;

    using HappyMe.Web.Controllers;

    using MoreDotNet.Extensions.Common;

    using Xunit;

    public class HomeControllerTests
    {
        private readonly HomeController homeController;

        public HomeControllerTests()
        {
            this.homeController = new HomeController();
        }

        [Fact]
        public void Index_SouldReturnEmptyView()
        {
            var result = this.homeController.Index();
            Assert.IsType<ViewResult>(result);
            var castResult = result.As<ViewResult>();
            Assert.Null(castResult.ViewData.Model);
        }
    }
}
