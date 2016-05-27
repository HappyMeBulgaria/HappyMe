namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Services.Data.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.InputModels.Users;
    using Te4Fest.Web.Areas.Administration.ViewModels.Users;
    using Te4Fest.Web.Common.Extensions;

    public class UsersController : MvcGridAdministrationController<User, UserGridViewModel, UserCreateInputModel, UserUpdateInputModel>
    {
        private readonly UserManager<User> userManager;

        public UsersController(
            IUsersDataService userData,
            IAdministrationService<User> userAdministrationService,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData, userAdministrationService, mappingService)
        {
            this.userManager = userManager;
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(u => u.Id));

        [HttpGet]
        public ActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateInputModel model)
        {
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
    }
}