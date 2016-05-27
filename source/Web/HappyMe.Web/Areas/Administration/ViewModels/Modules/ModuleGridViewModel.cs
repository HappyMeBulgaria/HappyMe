namespace HappyMe.Web.Areas.Administration.ViewModels.Modules
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Models;

    public class ModuleGridViewModel : IMapFrom<Module>, IMapTo<Module>, IIdentifiable<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
    }
}