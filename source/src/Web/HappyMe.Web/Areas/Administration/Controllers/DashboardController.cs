namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.Dashboard;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IMappingService mappingService;

        public DashboardController(
            IUsersDataService userData,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData, userManager)
        {
            this.mappingService = mappingService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserChildrenViewModels = this.mappingService.MapCollection<ChildViewModel>((await this.GetCurrentUserAsync()).Children.AsQueryable())
                .ToList();

            var indexViewModel = new DashboardIndexViewModel { CurrentUserChildren = currentUserChildrenViewModels };

            return this.View(indexViewModel);
        }

        public async Task<IActionResult> LoginChild(string username)
        {
            await this.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Login", "Account", new { area = string.Empty, username = username });
        }
    }
}
