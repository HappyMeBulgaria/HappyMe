namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.ViewModels.Modules;

    public class ModulesController : MvcGridAdministrationController<Module, ModuleGridViewModel>
    {
        public ModulesController(
            ModulesAdministrationService modulesAdministrationService, 
            IMappingService mappingService)
            : base(modulesAdministrationService, mappingService)
        {
        }

        public ActionResult Index() => this.View();
    }
}