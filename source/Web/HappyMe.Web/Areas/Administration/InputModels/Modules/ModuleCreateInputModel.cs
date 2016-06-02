namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleCreateInputModel : IMapTo<Module>, IMapFrom<Module>
    {
        [Display(Name = "Име на модул")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Описание на модул")]
        [UIHint("CKeditor")]
        public string Description { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
        
        [Display(Name = "Активен ли е модула?")]
        public bool IsActive { get; set; }
    }
}