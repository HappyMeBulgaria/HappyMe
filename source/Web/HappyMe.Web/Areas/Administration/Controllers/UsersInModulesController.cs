namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Common.Extensions;

    using Models.Areas.Administration.InputModels.UsersInModules;
    using Models.Areas.Administration.ViewModels.UsersInModules;

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
            IAdministrationService<Module> modulesAdministrationService)
            : base(userData, dataRepository, mappingService)
        {
            this.usersAdministrationService = usersAdministrationService;
            this.modulesAdministrationService = modulesAdministrationService;
        }

        public ActionResult Index() => this.View(this.GetData().OrderByDescending(x => x.CreatedOn));

        [HttpGet]
        public ActionResult Create()
        {
            this.PopulateViewBagData();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInModuleCreateInputModel model)
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
        public ActionResult Delete(int id, string secondId)
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