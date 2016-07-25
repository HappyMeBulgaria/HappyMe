namespace HappyMe.Tests.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Services.Common.Mapping;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Controllers;
    using HappyMe.Web.ViewModels.Modules;

    using Moq;

    using MoreDotNet.Extentions.Common;

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
    }
}
