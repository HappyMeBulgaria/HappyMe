namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using HappyMe.Data.Models;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;

    using Microsoft.AspNet.Identity;

    using Models.Areas.Administration.ViewModels.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IMappingService mappingService;
        private readonly UserManager<User> userManager;

        public DashboardController(
            IUsersDataService userData,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData)
        {
            this.mappingService = mappingService;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            var currentUserChildrenViewModels = this.mappingService.MapCollection<ChildViewModel>(this.UserProfile.Children.AsQueryable())
                .ToList();

            var indexViewModel = new DashboardIndexViewModel { CurrentUserChildren = currentUserChildrenViewModels };

            return this.View(indexViewModel);
        }

        public ActionResult LoginChild(string username)
        {
            this.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction("Login", "Account", new { area = string.Empty, username = username });
        }
    }
}