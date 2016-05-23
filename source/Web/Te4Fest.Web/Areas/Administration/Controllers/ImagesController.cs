namespace Te4Fest.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;
    using Te4Fest.Web.Areas.Administration.Controllers.Base;
    using Te4Fest.Web.Areas.Administration.InputModels.Images;
    using Te4Fest.Web.Areas.Administration.ViewModels.Images;
    using Te4Fest.Web.Common.Extensions;

    public class ImagesController : MvcGridAdministrationController<Image, ImageGridViewModel, ImageCreateInputModel, ImageUpdateInputModel>
    {
        public ImagesController(
            IAdministrationService<Image> imageAdministrationService,
            IMappingService mapingService) : base(imageAdministrationService, mapingService)
        {
        }

        public ActionResult Index() => this.View(this.GetData().OrderBy(x => x.Id));

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageCreateInputModel input)
        {
            var entity = this.BaseCreate(input);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно запазихте изображение");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(input);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ImageUpdateInputModel input)
        {
            var entity = this.BaseUpdate(input, input.Id);
            if (entity != null)
            {
                this.TempData.AddSuccessMessage("Успешно редактирахте модул");
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                this.TempData.AddDangerMessage("Няма такава снимка.");
                return this.RedirectToAction<ImagesController>(x => x.Index());
            }

            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте изображение.");
            return this.RedirectToAction<ImagesController>(x => x.Index());
        }
    }
}