namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;
    using HappyMe.Web.Common.Attributes;

    public class ModuleCreateInputModel : IMapTo<Module>, IMapFrom<Module>
    {
        [Display(Name = "Име на модул")]
        [PlaceHolder("Име на модул")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Описание на модул")]
        [UIHint("CKeditor")]
        [PlaceHolder("Описание на модул")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Display(Name = "Активен?")]
        public bool IsActive { get; set; }

        [Display(Name = "Публичен?")]
        public bool IsPublic { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Моля изберете изображение.")]
        [UIHint("ImageUpload")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}