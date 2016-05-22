namespace Te4Fest.Web.Areas.Administration.InputModels.Modules
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class ModuleCreateInputModel : IMapTo<Module>, IMapFrom<Module>
    {
        public string Name { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}