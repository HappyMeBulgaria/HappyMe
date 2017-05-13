namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Modules;
    using HappyMe.Web.Areas.Administration.ViewModels.Modules;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ModulesController :
        MvcGridAdministrationCrudController<Module, ModuleGridViewModel, ModuleCreateInputModel, ModuleUpdateInputModel>
    {
        private readonly IImagesAdministrationService imagesAdministrationService;

        public ModulesController(
            IUsersDataService userData,
            IModulesAdministrationService modulesAdministrationService,
            IMappingService mappingService,
            IImagesAdministrationService imagesAdministrationService,
            UserManager<User> userManager)
            : base(userData, modulesAdministrationService, mappingService, userManager)
        {
            this.imagesAdministrationService = imagesAdministrationService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ModuleGridViewModel> modules;
            if (this.User.IsAdmin())
            {
                modules = this.GetData().OrderBy(x => x.Id);
            }
            else
            {
                modules =
                    this.MappingService.MapCollection<ModuleGridViewModel>(
                        (this.AdministrationService as IModulesAdministrationService)
                        ?.GetUserAndPublicModules(await this.GetUserIdAsync()))
                        .OrderBy(m => m.Id);
            }

            return this.View(modules);
        }

        [HttpGet]
        public ActionResult Create() => this.View(new ModuleCreateInputModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModuleCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.AuthorId = await this.GetUserIdAsync();

                if (model.ImageFile != null)
                {
                    model.ImageId = this.imagesAdministrationService.Create(model.ImageFile.ToByteArray(), model.AuthorId).Id;
                }

                var entity = this.BaseCreate(model);
                if (entity != null)
                {
                    this.TempData.AddSuccessMessage("Успешно създадохте модул");
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв модул.");
            }

            var userHasRights = await this.CheckIfUserHasRights(id.Value);

            if (userHasRights)
            {
                return this.View(this.GetEditModelData(id));
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този модул");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ModuleUpdateInputModel model)
        {
            var userHasRights = await this.CheckIfUserHasRights(model.Id);

            if (userHasRights)
            {
                if (this.ModelState.IsValid)
                {
                    if (model.ImageFile != null)
                    {
                        model.ImageId = this.imagesAdministrationService.Update(
                            model.ImageFile.ToByteArray(),
                            model.ImageId,
                            await this.GetUserIdAsync(),
                            this.User.IsAdmin()).Id;
                    }

                    var entity = this.BaseUpdate(model, model.Id);
                    if (entity != null)
                    {
                        this.TempData.AddSuccessMessage("Успешно редактирахте модул");
                        return this.RedirectToAction(nameof(this.Index));
                    }
                }

                return this.View(model);
            }

            this.TempData.AddDangerMessage("Нямате права за да променяте този модул");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такъв модул.");
            }

            var userHasRights = await this.CheckIfUserHasRights(id.Value);

            if (userHasRights)
            {
                this.BaseDestroy(id);

                this.TempData.AddSuccessMessage("Успешно изтрихте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData.AddDangerMessage("Нямате права за да изтривате този модул");
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> CheckIfUserHasRights(int moduleId)
        {
            return this.User.IsAdmin() ||
                ((this.AdministrationService as IModulesAdministrationService)
                    ?.CheckIfUserIsModuleAuthor(moduleId, await this.GetUserIdAsync()) ?? false);
        }
    }
}
