namespace HappyMe.Web.Controllers
{
    using System.Web.Mvc;

    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;
    using HappyMe.Web.Controllers.Base;

    public class ModulesController : BaseController
    {
        private readonly IModulesDataService modulesDataService;

        public ModulesController(IModulesDataService modulesDataService)
        {
            this.modulesDataService = modulesDataService;
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

            // TODO: Convert to VM
            return this.View();
        }
    }
}