namespace HappyMe.Tests.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Controllers;
    using HappyMe.Web.ViewModels.Modules;

    using Moq;

    using MoreDotNet.Extensions.Common;

    using Xunit;

    using Module = HappyMe.Data.Models.Module;

    public class ModulesControllerTests : BaseWebTests
    {
        private readonly ModulesController modulesController;
        private readonly Mock<IModulesDataService> modulesDataServiceMock;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly Mock<IMappingService> mappingServiceMock;

        public ModulesControllerTests()
        {
            IModuleSessionDataService moduleSessionDataService;
            this.modulesDataServiceMock = new Mock<IModulesDataService>();

            this.moduleSessionDataService = new Mock<IModuleSessionDataService>().Object;

            this.mappingServiceMock = new Mock<IMappingService>();

            this.modulesController = new ModulesController(
                this.modulesDataServiceMock.Object,
                this.moduleSessionDataService,
                this.mappingServiceMock.Object);
        }

        [Fact]
        public void Index_ShouldReturnViewWithAllPublicMoules()
        {
            var samplePublicModules = new HashSet<Module>
            {
                new Module { Id = 1, IsPublic = true },
                new Module { Id = 2, IsPublic = true },
                new Module { Id = 3, IsPublic = true }
            };

            var simpleViewModels = new HashSet<ModuleViewModel>
            {
                new ModuleViewModel { Id = 1 },
                new ModuleViewModel { Id = 2 },
                new ModuleViewModel { Id = 3 }
            };

            this.modulesDataServiceMock.Setup(x => x.AllPublicWithQuestionsWithCorrectAnswer()).Returns(() => samplePublicModules.AsQueryable());

            this.mappingServiceMock.Setup(x => x.MapCollection<ModuleViewModel>(It.IsAny<IQueryable<Module>>(), null))
                .Returns(() => simpleViewModels.AsQueryable());

            var result = this.modulesController.Index();
            Assert.IsType<ViewResult>(result);
            var castResult = result.As<ViewResult>();

            var model = castResult.ViewData.Model.As<IEnumerable<ModuleViewModel>>();

            Assert.NotNull(model);
            Assert.NotEmpty(model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Start_ShouldRedirectModulesIndexIfGivenNullId()
        {
            var result = await this.modulesController.Start(null);

            this.ValidateRedirectToModulesIndex(result);
        }

        [Fact]
        public async Task Start_ShouldRedirectModulesIndexIfGivenInvalidId()
        {
            this.modulesDataServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => null);

            var result = await this.modulesController.Start(null);

            this.ValidateRedirectToModulesIndex(result);
        }

        [Fact]
        public async Task Start_ShouldRedirectModulesIndexIfGivenModuleIsInactive()
        {
            var inactiveModule = new Module { IsActive = false };

            this.modulesDataServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => inactiveModule);

            var result = await this.modulesController.Start(null);

            this.ValidateRedirectToModulesIndex(result);
        }

        [Fact]
        public async Task Start_ShouldRedirectModulesIndexIfGivenModuleIsNotPublic()
        {
            var inactiveModule = new Module { IsActive = true, IsPublic = false };

            this.modulesDataServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => inactiveModule);

            var result = await this.modulesController.Start(null);

            this.ValidateRedirectToModulesIndex(result);
        }

        [Fact]
        public void Success_SouldReturnEmptyView()
        {
            var result = this.modulesController.Success();
            Assert.IsType<ViewResult>(result);
            var castResult = result.As<ViewResult>();
            Assert.Null(castResult.ViewData.Model);
        }

        private void ValidateRedirectToModulesIndex(ActionResult result)
        {
            Assert.IsType<RedirectToRouteResult>(result);

            var castResult = result.As<RedirectToRouteResult>();

            Assert.Equal(1, this.modulesController.TempData.Keys.Count);
            Assert.True(this.modulesController.TempData.ContainsKey(GlobalConstants.DangerMessage));
            Assert.Equal("Упс! Няма такъв модул.", this.modulesController.TempData[GlobalConstants.DangerMessage]);

            Assert.Equal("Index", castResult.RouteValues["Action"]);
            Assert.Equal("Modules", castResult.RouteValues["Controller"]);
            Assert.Equal(string.Empty, castResult.RouteValues["Area"]);
        }
    }
}
