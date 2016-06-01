namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Modules;
    using HappyMe.Web.Areas.Administration.ViewModels.Modules;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;
    
    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class ModulesController : 
        MvcGridAdministrationCrudController<Module, ModuleGridViewModel, ModuleCreateInputModel, ModuleUpdateInputModel>
    {
        public ModulesController(
            IUsersDataService userData,
            IModulesAdministrationService modulesAdministrationService, 
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
                        (this.AdministrationService as IModulesAdministrationService)
                            .GetUserAndPublicModules(this.UserProfile.Id))
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
        
        [HttpGet]
        public ActionResult Update(int id)
        {
            var userHasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IModulesAdministrationService)
                    .CheckIfUserIsModuleAuthor(id, this.UserProfile.Id);
            if (userHasRights)
            {
                return this.View(this.GetEditModelData(id));
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този модул");
            return this.RedirectToAction(nameof(this.Index));
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ModuleUpdateInputModel model)
        {
            var userHasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IModulesAdministrationService)
                    .CheckIfUserIsModuleAuthor(model.Id, this.UserProfile.Id);
            if (userHasRights)
            {
                var entity = this.BaseUpdate(model, model.Id);
                if (entity != null)
                {
                    this.TempData.AddSuccessMessage("Успешно редактирахте модул");
                    return this.RedirectToAction(nameof(this.Index));
                }

                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този модул");
            return this.RedirectToAction(nameof(this.Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userHasRights = this.User.IsAdmin() ||
                (this.AdministrationService as IModulesAdministrationService)
                    .CheckIfUserIsModuleAuthor(id, this.UserProfile.Id);
            if (userHasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате права за да изтривате този модул");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}