namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Base;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.ViewModels.Modules;
    using Te4Fest.Web.Controllers.Base;

    public class ModulesController : BaseController
    {
        private readonly AdministrationService<Module> moduleService;
        private readonly IMappingService mappingService;

        public ModulesController(AdministrationService<Module> moduleService, IMappingService mappingService)
        {
            this.moduleService = moduleService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var model = this.mappingService.MapCollection<ModuleGridViewModel>(this.moduleService.Read());
            return this.View(model);
        }
    }
}