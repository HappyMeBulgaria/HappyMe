namespace Te4Fest.Web.Areas.Administration.ViewModels.Modules
{
    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class ModuleGridViewModel : IMapFrom<Module>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string AuthorId { get; set; }
    }
}