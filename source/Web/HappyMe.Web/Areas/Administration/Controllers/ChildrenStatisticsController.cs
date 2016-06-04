namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics;

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
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
            }

            return this.View();
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

            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllForParent(string id)
        {
            var viewModel = new AllParentStatisticsViewModel
            {
                ModulePlayedTimesStatisticsFull = this.childrenStatisticsService.GetModulePlayedTimesStatisticsForParentsChildren(id)
            };

            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}