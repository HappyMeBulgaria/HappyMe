namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.InputModels.Users;
    using HappyMe.Web.Areas.Administration.ViewModels.Users;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;


    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class UsersController :
        MvcGridAdministrationCrudController<User, UserGridViewModel, UserCreateInputModel, UserUpdateInputModel>
    {
        private readonly IAdministrationService<IdentityRole> roleAdministrationService;
        private readonly UsersInRolesAdministrationService usersInRolesAdministrationService;

        public UsersController(
            IUsersDataService userData,
            IUsersAdministrationService userAdministrationService,
            IMappingService mappingService,
            IAdministrationService<IdentityRole> roleAdministrationService,
            UsersInRolesAdministrationService usersInRolesAdministrationService,
            UserManager<User> userManager)
            : base(userData, userAdministrationService, mappingService, userManager)
        {
            this.roleAdministrationService = roleAdministrationService;
            this.usersInRolesAdministrationService = usersInRolesAdministrationService;
        }

        [AuthorizeRoles(RoleConstants.Administrator)]
        public IActionResult Index() => this.View(this.GetData().OrderBy(u => u.Id));

        [HttpGet]
        public IActionResult Create() => this.View(new UserCreateInputModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateInputModel model)
        {
            // TODO: Conform the case if model.IsSamePassword is true
            var entity = this.MappingService.Map<User>(model);
            entity.ParentId = await this.GetUserIdAsync();
            var userCreateResult = await this.UserManager.CreateAsync(entity, model.Password);
            if (this.ModelState.IsValid)
            {
                var roleAssigned = await this.UserManager.AddToRoleAsync(entity, RoleConstants.Child);
                (await this.GetCurrentUserAsync()).Children.Add(entity);

                if (userCreateResult.Succeeded && roleAssigned.Succeeded)
                {
                    this.TempData.AddSuccessMessage("Успешно създадохте потребител");
                    return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
                }

                this.TempData.AddDangerMessage(string.Join(";", userCreateResult.Errors));
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpGet]
        [AuthorizeRoles(RoleConstants.Administrator)]
        public IActionResult Update(string id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleConstants.Administrator)]
        public IActionResult Update(UserUpdateInputModel model)
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
        public IActionResult Delete(string id)
        {
            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте потребител");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [AuthorizeRoles(RoleConstants.Administrator)]
        public IActionResult AddUserInRole(string id)
        {
            // TODO: Don't get role, if user is in it
            var roles = this.roleAdministrationService
                .Read()
                .Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
                .ToList();

            if (!roles.Any())
            {
                this.TempData.AddWarningMessage("Няма намерени роли");
                return this.RedirectToAction(nameof(this.Index));
            }

            var user = this.AdministrationService.Get(id);
            var model = this.MappingService.Map<AddUserInRoleInputModel>(user);

            this.ViewBag.RoleIdData = roles;
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUserInRole(AddUserInRoleInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userInRole = this.MappingService.Map<IdentityUserRole<string>>(model);
                this.usersInRolesAdministrationService.Create(userInRole);

                this.TempData.AddSuccessMessage("Успешно добавихте потребител в роля");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }
    }
}