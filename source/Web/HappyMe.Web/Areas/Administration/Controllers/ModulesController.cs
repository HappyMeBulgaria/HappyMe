namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
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

    // TODO: Add authorization for admin and parrent roles
    public class ModulesController : 
        MvcGridAdministrationCrudController<Module, ModuleGridViewModel, ModuleCreateInputModel, ModuleUpdateInputModel>
    {
        public ModulesController(
            IUsersDataService userData,
            ModulesAdministrationService modulesAdministrationService, 
            IMappingService mappingService)
            : base(userData, modulesAdministrationService, mappingService)
        {
        }

        // Pass different modules depend of role of the user
        public ActionResult Index()
        {
            IEnumerable<ModuleGridViewModel> modules;
            if (this.User.IsAdmin())
            {
                modules = this.GetData().OrderBy(x => x.Id);
            }
            else
            {
                modules = this.MappingService
                    .MapCollection<ModuleGridViewModel>(
                        (this.AdministrationService as ModulesAdministrationService)
                            .GetUserModules(this.UserProfile.Id))
                    .OrderBy(m => m.Id);
            }

            return this.View(modules);
        } 

        [HttpGet]
        public ActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleCreateInputModel model)
        {
            model.AuthorId = this.UserProfile.Id;
            var entity = this.BaseCreate(model);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно създадохте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // TODO: Validate if user has rights to edit this module
        [HttpGet]
        public ActionResult Update(int id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ModuleUpdateInputModel model)
        {
            // TODO: Validate if user has rights to edit this module
            var entity = this.BaseUpdate(model, model.Id);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно редактирахте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // TODO: Validate if user has rights to delete this module
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