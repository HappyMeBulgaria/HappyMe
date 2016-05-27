namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Modules;
    using HappyMe.Web.Areas.Administration.ViewModels.Modules;
    using HappyMe.Web.Common.Extensions;

    public class ModulesController : 
        MvcGridAdministrationController<Module, ModuleGridViewModel, ModuleCreateInputModel, ModuleUpdateInputModel>
    {
        public ModulesController(
            IUsersDataService userData,
            ModulesAdministrationService modulesAdministrationService, 
            IMappingService mappingService)
            : base(userData, modulesAdministrationService, mappingService)
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
                this.TempData.AddSuccessMessage("Успешно създадохте модул");
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
                this.TempData.AddSuccessMessage("Успешно редактирахте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте модул");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}