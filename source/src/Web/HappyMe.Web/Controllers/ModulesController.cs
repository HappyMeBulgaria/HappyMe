namespace HappyMe.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;
    using HappyMe.Web.ViewModels.Modules;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ModulesController : BaseController
    {
        private readonly IModulesDataService modulesDataService;
        private readonly IModuleSessionDataService moduleSessionDataService;
        private readonly IMappingService mappingService;

        public ModulesController(
            IModulesDataService modulesDataService,
            IModuleSessionDataService moduleSessionDataService,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userManager)
        {
            this.modulesDataService = modulesDataService;
            this.moduleSessionDataService = moduleSessionDataService;
            this.mappingService = mappingService;
        }

        public IActionResult Index()
        {
            var modules = this.modulesDataService.AllPublicWithQuestionsWithCorrectAnswer();

            var viewModels = this.mappingService
                .MapCollection<ModuleViewModel>(modules)
                .ToList();

            return this.View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Start(int? id)
        {
            if (!id.HasValue)
            {
                this.TempData.AddDangerMessage("Няма такъв модул");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            var module = this.modulesDataService.GetById(id.Value);

            if (module == null)
            {
                this.TempData.AddDangerMessage("Няма такъв модул");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            if (!module.IsActive || !module.IsPublic)
            {
                this.TempData.AddDangerMessage("Няма такъв модул");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            ModuleSession session;

            if (this.User.IsLoggedIn())
            {
                session = await this.moduleSessionDataService.StartUserSession(await this.GetUserIdAsync(), id.Value);
            }
            else
            {
                session = await this.moduleSessionDataService.StartAnonymousSession(id.Value);
            }

            return this.RedirectToAction("Answer", "Questions", new { area = string.Empty, id = session.Id });
        }

        [HttpGet]
        public IActionResult Success()
        {
            return this.View();
        }
    }
}
