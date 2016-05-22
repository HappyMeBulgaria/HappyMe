namespace Te4Fest.Web.Areas.Administration.ViewModels.Modules
{
    using System.Web.Mvc;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Contracts;
    using Te4Fest.Data.Models;

    public class ModuleGridViewModel : IMapFrom<Module>, IMapTo<Module>, IIdentifiable<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
    }
}