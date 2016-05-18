namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Mvc.JQuery.DataTables;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Areas.Administration.ViewModels.Images;

    using Web.Controllers.Base;

    public class ImagesController : BaseController
    {
        private readonly IAdministrationService<Image> imageAdministrationService;
        private readonly IMappingService mapingService;

        public ImagesController(
            IAdministrationService<Image> imageAdministrationService,
            IMappingService mapingService)
        {
            this.imageAdministrationService = imageAdministrationService;
            this.mapingService = mapingService;
        }

        public ActionResult Index()
        {
            var getDataUrl = this.Url.Action(nameof(ImagesController.GetImages));
            var vm = DataTablesHelper.DataTableVm<ImageGridViewModel>("imagesGridTable", getDataUrl);

            return this.View(vm);
        }

        public DataTablesResult<ImageGridViewModel> GetImages(DataTablesParam dataTableParam)
        {
            var imagesViewModels = this.mapingService.MapCollection<ImageGridViewModel>(this.imageAdministrationService.Read());
            return DataTablesResult.Create(imagesViewModels, dataTableParam);
        }
    }
}