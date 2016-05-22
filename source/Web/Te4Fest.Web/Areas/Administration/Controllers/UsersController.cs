namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.InputModels.Users;
    using Te4Fest.Web.Areas.Administration.ViewModels.Users;

    public class UsersController : MvcGridAdministrationController<User, UserGridViewModel, UserCreateInputModel, UserEditInputModel>
    {
        public UsersController(
            IAdministrationService<User> userAdministrationService,
            IMappingService mappingService)
            : base(userAdministrationService, mappingService)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(u => u.Id));
    }
}