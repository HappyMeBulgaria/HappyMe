namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics;
    using HappyMe.Web.Common.Extensions;

    public class ChildrenStatisticsController : AdministrationController
    {
        private readonly IChildrenStatisticsService childrenStatisticsService;

        public ChildrenStatisticsController(
            IUsersDataService userData,
            IChildrenStatisticsService childrenStatisticsService)
            : base(userData)
        {
            this.childrenStatisticsService = childrenStatisticsService;
        }

        [HttpGet]
        public ActionResult Index(string username)
        {
            var child = this.UsersData.GetUserByUsername(username);
            if (child == null)
            {
                return this.ItemNotFound("Няма такова дете.");
            }

            if (child.ParentId != this.UserProfile.Id)
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
        public ActionResult AllForChild(string id)
        {
            var viewModel = new AllChildStatisticsViewModel
            {
                ModuleSessionStatistics = this.childrenStatisticsService.GetModuleSessionStatistics(id).ToList(),
                ChildAnswerRatoStatistics = this.childrenStatisticsService.GetWrongRightAnswersStatistics(id).ToList(),
                ModulePlayedTimesStatistics = this.childrenStatisticsService.GetModulePlayedTimesStatistics(id).ToList()
            };

            return this.JsonCamelCase(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllForParent(string id)
        {
            var viewModel = new AllParentStatisticsViewModel
            {
                ModulePlayedTimesStatisticsFull = this.childrenStatisticsService.GetModulePlayedTimesStatisticsForParentsChildren(id)
            };

            return this.JsonCamelCase(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}