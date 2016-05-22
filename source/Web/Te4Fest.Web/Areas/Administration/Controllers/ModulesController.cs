namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Te4Fest.Common.Constants;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.InputModels.Modules;
    using Te4Fest.Web.Areas.Administration.ViewModels.Modules;

    public class ModulesController : 
        MvcGridAdministrationController<Module, ModuleGridViewModel, ModuleCreateInputModel, ModuleUpdateInputModel>
    {
        public ModulesController(
            ModulesAdministrationService modulesAdministrationService, 
            IMappingService mappingService)
            : base(modulesAdministrationService, mappingService)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(x => x.Id));

        [HttpGet]
        public ActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleCreateInputModel model)
        {
            var entity = this.BaseCreate(model);
            if (entity != null)
            {
                this.TempData[GlobalConstants.SuccessMessage] = "Успешно създадохте модул";
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(int id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ModuleUpdateInputModel model)
        {
            var entity = this.BaseUpdate(model, model.Id);
            if (entity != null)
            {
                this.TempData[GlobalConstants.SuccessMessage] = "Успешно редактирахте модул";
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }
    }
}