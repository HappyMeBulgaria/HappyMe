namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models.Areas.Administration.InputModels.Users;
    using Models.Areas.Administration.ViewModels.Users;

    [AuthorizeRoles(RoleConstants.Administrator, RoleConstants.Parent)]
    public class UsersController : 
        MvcGridAdministrationCrudController<User, UserGridViewModel, UserCreateInputModel, UserUpdateInputModel>
    {
        private readonly IAdministrationService<IdentityRole> roleAdministrationService;
        private readonly UsersInRolesAdministrationService usersInRolesAdministrationService;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUsersDataService userData,
            IUsersAdministrationService userAdministrationService,
            IMappingService mappingService,
            IAdministrationService<IdentityRole> roleAdministrationService,
            UsersInRolesAdministrationService usersInRolesAdministrationService,
            UserManager<User> userManager)
            : base(userData, userAdministrationService, mappingService)
        {
            this.roleAdministrationService = roleAdministrationService;
            this.usersInRolesAdministrationService = usersInRolesAdministrationService;
            this.userManager = userManager;
        }

        [AuthorizeRoles(RoleConstants.Administrator)]
        public ActionResult Index() => this.View(this.GetData().OrderBy(u => u.Id));

        [HttpGet]
        public ActionResult Create() => this.View(new UserCreateInputModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateInputModel model)
        {
            // TODO: Conform the case if model.IsSamePassword is true
            var entity = this.MappingService.Map<User>(model);
            entity.ParentId = this.UserProfile.Id;
            var userCreateResult = this.userManager.Create(entity, model.Password);
            if (this.ModelState.IsValid)
            {
                var roleAssigned = this.userManager.AddToRole(entity.Id, RoleConstants.Child);
                this.UserProfile.Children.Add(entity);

                if (userCreateResult.Succeeded && roleAssigned.Succeeded)
                {
                    this.TempData.AddSuccessMessage("Успешно създадохте потребител");
                    return this.RedirectToAction<DashboardController>(c => c.Index());
                }

                this.TempData.AddDangerMessage(string.Join(";", userCreateResult.Errors));
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpGet]
        [AuthorizeRoles(RoleConstants.Administrator)]
        public ActionResult Update(string id) => this.View(this.GetEditModelData(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleConstants.Administrator)]
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

            this.TempData.AddSuccessMessage("Успешно изтрихте потребител");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [AuthorizeRoles(RoleConstants.Administrator)]
        public ActionResult AddUserInRole(string id)
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
        public ActionResult AddUserInRole(AddUserInRoleInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userInRole = this.MappingService.Map<IdentityUserRole>(model);
                this.usersInRolesAdministrationService.Create(userInRole);

                this.TempData.AddSuccessMessage("Успешно добавихте потребител в роля");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }
    }
}