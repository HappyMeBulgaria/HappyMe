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
        private readonly AdministrationService<Module> modulesAdministrationService;
        private readonly IMappingService mappingService;

        public ModulesController(
            AdministrationService<Module> modulesAdministrationService, 
            IMappingService mappingService)
        {
            this.mappingService = mappingService;
            this.modulesAdministrationService = modulesAdministrationService;
        }

        public ActionResult Index()
        {
            var model = this.mappingService.MapCollection<ModuleGridViewModel>(this.modulesAdministrationService.Read());
            return this.View(model);
        }
    }
}