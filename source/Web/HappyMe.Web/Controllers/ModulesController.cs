namespace HappyMe.Web.Controllers
{
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
        public ActionResult Start(int? id)
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

            if (this.User.IsLoggedIn())
            {
                this.moduleSessionDataService.StartUserSession(this.User.Identity.GetUserId(), id.Value);
            }
            else
            {
                this.moduleSessionDataService.StartAnonymousSession(id.Value);
            }
            
            return this.View();
        }
    }
}