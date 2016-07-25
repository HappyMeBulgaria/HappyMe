namespace HappyMe.Tests.Web
{
    using HappyMe.Services.Common.Mapping;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Tests.Web.Common;
    using HappyMe.Web.Controllers;

    using Moq;

    public class ModulesControllerTests : BaseWebTests
    {
        private readonly ModulesController modulesController;
        private readonly IModulesDataService modulesDataService;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly IMappingService mappingService;

        public ModulesControllerTests()
        {
            this.modulesDataService = new Mock<IModulesDataService>().Object;

            this.moduleSessionDataService = new Mock<IModuleSessionDataService>().Object;

            this.mappingService = new AutoMapperMappingService();

            this.modulesController = new ModulesController(
                this.modulesDataService,
                this.moduleSessionDataService,
                this.mappingService);
        }
    }
}
