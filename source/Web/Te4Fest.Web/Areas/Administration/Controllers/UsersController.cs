namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Controllers.Base;

    public class UsersController : BaseController
    {
        private IAdministrationService<User> userAdministrationService;
        private IMappingService mappingService; 

        public UsersController(
            IAdministrationService<User> userAdministrationService,
            IMappingService mappingService)
        {
            this.userAdministrationService = userAdministrationService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}