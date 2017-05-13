namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.UsersInModules;
    using HappyMe.Web.Areas.Administration.ViewModels.UsersInModules;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UsersInModulesController :
        MvcGridAdministrationCrudController<UserInModule, UserInModuleGridViewModel, UserInModuleCreateInputModel, UserInModuleCreateInputModel>
    {
        private readonly IAdministrationService<User> usersAdministrationService;
        private readonly IAdministrationService<Module> modulesAdministrationService;

        public UsersInModulesController(
            IUsersDataService userData,
            IAdministrationService<UserInModule> dataRepository,
            IMappingService mappingService,
            IAdministrationService<User> usersAdministrationService,
            IAdministrationService<Module> modulesAdministrationService,
            UserManager<User> userManager)
            : base(userData, dataRepository, mappingService, userManager)
        {
            this.usersAdministrationService = usersAdministrationService;
            this.modulesAdministrationService = modulesAdministrationService;
        }

        public IActionResult Index() => this.View(this.GetData().OrderByDescending(x => x.CreatedOn));

        [HttpGet]
        public IActionResult Create()
        {
            this.PopulateViewBagData();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserInModuleCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.BaseCreate(model);
                if (entity != null)
                {
                    this.TempData.AddSuccessMessage("Успешно добавихте потребител в модул");
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string secondId)
        {
            this.BaseDestroy(secondId, id);

            this.TempData.AddSuccessMessage("Успешно изтрихте потребител в модул");
            return this.RedirectToAction(nameof(this.Index));
        }

        private void PopulateViewBagData()
        {
            this.ViewBag.UserIdData = this.usersAdministrationService
                .Read()
                .Select(u => new SelectListItem { Text = u.UserName, Value = u.Id });
            this.ViewBag.ModuleIdData = this.modulesAdministrationService
                .Read()
                .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
        }
    }
}
