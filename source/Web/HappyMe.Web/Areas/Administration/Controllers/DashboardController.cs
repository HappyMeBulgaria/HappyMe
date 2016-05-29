namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IMappingService mappingService;

        public DashboardController(
            IUsersDataService userData,
            IMappingService mappingService)
            : base(userData)
        {
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var currentUserChildrenViewModels = this.mappingService.MapCollection<ChildViewModel>(this.UserProfile.Children.AsQueryable())
                .ToList();

            var indexViewModel = new DashboardIndexViewModel { CurrentUserChildren = currentUserChildrenViewModels };

            return this.View(indexViewModel);
        }
    }
}