namespace HappyMe.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Areas.Administration.Controllers.Base;
    using HappyMe.Web.Common.Attributes;
    using HappyMe.Web.Common.Extensions;

    using Models.Areas.Administration.InputModels.Images;
    using Models.Areas.Administration.ViewModels.Images;

    [AuthorizeRoles(RoleConstants.Administrator)]
    public class ImagesController : 
        MvcGridAdministrationCrudController<Image, ImageGridViewModel, ImageCreateInputModel, ImageUpdateInputModel>
    {
        public ImagesController(
            IUsersDataService userData,
            IAdministrationService<Image> imageAdministrationService,
            IMappingService mapingService) 
            : base(userData, imageAdministrationService, mapingService)
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
        public ActionResult Update(int? id)
        {
            if (!id.HasValue)
            {
                return this.ItemNotFound("Няма такава снимка.");
            }

            var editModel = this.GetEditModelData(id);
            if (editModel == null)
            {
                this.TempData.AddDangerMessage("Няма такава снимка.");
                return this.ItemNotFound("Няма такава снимка.");
            }

            return this.View(editModel);
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
                return this.ItemNotFound("Няма такава снимка.");
            }

            this.BaseDestroy(id);

            this.TempData.AddSuccessMessage("Успешно изтрихте изображение.");
            return this.RedirectToAction<ImagesController>(x => x.Index());
        }
    }
}