namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.InputModels.Users;
    using Te4Fest.Web.Areas.Administration.ViewModels.Users;
    using Te4Fest.Web.Common.Extensions;

    public class UsersController : MvcGridAdministrationController<User, UserGridViewModel, UserCreateInputModel, UserUpdateInputModel>
    {
        private readonly IAdministrationService<Role> roleAdministrationService;
        private readonly UsersInRolesAdministrationService usersInRolesAdministrationService;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUsersAdministrationService userAdministrationService,
            IUsersDataService userData,
            IAdministrationService<User> userAdministrationService,
            IMappingService mappingService,
            IAdministrationService<Role> roleAdministrationService,
            UsersInRolesAdministrationService usersInRolesAdministrationService,
            UserManager<User> userManager)
            : base(userData, userAdministrationService, mappingService)
        {
            this.roleAdministrationService = roleAdministrationService;
            this.usersInRolesAdministrationService = usersInRolesAdministrationService;
            this.userManager = userManager;
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(u => u.Id));

        [HttpGet]
        public ActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateInputModel model)
        {
            // TODO: Conform the case if model.IsSamePassword is true
            var entity = MappingService.Map<User>(model);
            var userCreateResult = this.userManager.Create(entity, model.Password);
            if (this.ModelState.IsValid)
            {
                if (userCreateResult.Succeeded)
                {
                    this.TempData.AddSuccessMessage("Успешно създадохте потребител");
                    return this.RedirectToAction(nameof(this.Index));
                }

                this.TempData.AddDangerMessage(string.Join(";", userCreateResult.Errors));
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(string id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserUpdateInputModel model)
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
        public ActionResult Delete(string id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте модул");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public ActionResult AddUserInRole(string username)
        {
            var user = (this.AdministrationService as IUsersAdministrationService).GetByUsername(username);
            var model = this.MappingService.Map<AddUserInRoleInputModel>(user);

            this.ViewBag.RoleIdData = this.roleAdministrationService
                .Read()
                .Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
                .ToList();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserInRole(AddUserInRoleInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userInRole = this.MappingService.Map<UserInRole>(model);
                this.usersInRolesAdministrationService.Create(userInRole);

                this.TempData.AddSuccessMessage("Успешно добавихте потребител в роля");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }
    }
}