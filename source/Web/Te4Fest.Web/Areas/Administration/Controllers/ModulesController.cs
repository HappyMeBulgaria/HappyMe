namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.ViewModels.Modules;
    using Te4Fest.Web.Controllers.Base;

    public class ModulesController : BaseController
    {
        private readonly IModulesDataService modulesData;
        private readonly IMappingService mappingService;

        public ModulesController(IModulesDataService modulesData, IMappingService mappingService)
        {
            this.modulesData = modulesData;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var model = this.mappingService.MapCollection<ModuleGridViewModel>(this.modulesData.GetAllModules());
            return this.View(model);
        }
    }
}