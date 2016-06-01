namespace HappyMe.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;

    using Microsoft.AspNet.Identity;

    public class ModulesController : BaseController
    {
        private readonly IModulesDataService modulesDataService;
        private readonly IModuleSessionDataService moduleSessionDataService;

        public ModulesController(
            IModulesDataService modulesDataService,
            IModuleSessionDataService moduleSessionDataService)
        {
            this.modulesDataService = modulesDataService;
            this.moduleSessionDataService = moduleSessionDataService;
        }

        [HttpGet]
        public async Task<ActionResult> Start(int? id)
        {
            if (!id.HasValue)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            var module = this.modulesDataService.GetById(id.Value);

            if (module == null)
            {
                this.TempData.AddDangerMessage("Упс! Няма такъв модул.");
                return this.RedirectToAction("Index", "Modules", new { area = string.Empty });
            }

            int sessionId;

            if (this.User.IsLoggedIn())
            {
                sessionId = await this.moduleSessionDataService.StartUserSession(this.User.Identity.GetUserId(), id.Value);
            }
            else
            {
                sessionId = await this.moduleSessionDataService.StartAnonymousSession(id.Value);
            }
            
            return this.RedirectToAction("Answer", "Questions", new { area = string.Empty, id = sessionId });
        }
    }
}