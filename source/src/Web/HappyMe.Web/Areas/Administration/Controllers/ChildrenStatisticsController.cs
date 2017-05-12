namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ChildrenStatisticsController : AdministrationController
    {
        private readonly IChildrenStatisticsService childrenStatisticsService;

        public ChildrenStatisticsController(
            IUsersDataService userData,
            IChildrenStatisticsService childrenStatisticsService,
            UserManager<User> userManager)
            : base(userData, userManager)
        {
            this.childrenStatisticsService = childrenStatisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string username)
        {
            var child = this.UsersData.GetUserByUsername(username);
            if (child == null)
            {
                return this.ItemNotFound("Няма такова дете.");
            }

            if (child.ParentId != await this.GetUserIdAsync())
            {
                return this.ItemNotFound("Няма такова дете.");
            }

            var viewModel = new ChildStatisticsIndexViewModel
            {
                ChildId = child.Id,
                ChildUserName = child.UserName
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AllForChild(string id)
        {
            var viewModel = new AllChildStatisticsViewModel
            {
                ModuleSessionStatistics = this.childrenStatisticsService.GetModuleSessionStatistics(id).ToList(),
                ChildAnswerRatoStatistics = this.childrenStatisticsService.GetWrongRightAnswersStatistics(id).ToList(),
                ModulePlayedTimesStatistics = this.childrenStatisticsService.GetModulePlayedTimesStatistics(id).ToList()
            };

            return this.JsonCamelCase(viewModel);
        }

        [HttpGet]
        public IActionResult AllForParent(string id)
        {
            var viewModel = new AllParentStatisticsViewModel
            {
                ModulePlayedTimesStatisticsFull = this.childrenStatisticsService.GetModulePlayedTimesStatisticsForParentsChildren(id)
            };

            return this.JsonCamelCase(viewModel);
        }
    }
}