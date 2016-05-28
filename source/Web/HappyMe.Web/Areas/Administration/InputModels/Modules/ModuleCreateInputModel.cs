namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleCreateInputModel : IMapTo<Module>, IMapFrom<Module>
    {
        public string Name { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        public bool IsActive { get; set; }
    }
}