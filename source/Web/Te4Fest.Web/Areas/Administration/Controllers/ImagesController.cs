namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;

    using Web.Controllers.Base;

    public class ImagesController : BaseController
    {
        private readonly IAdministrationService<Image> imageAdministrationService; 

        public ImagesController(IAdministrationService<Image> imageAdministrationService)
        {
            this.imageAdministrationService = imageAdministrationService;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}