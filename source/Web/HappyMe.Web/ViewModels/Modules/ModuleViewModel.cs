namespace HappyMe.Web.ViewModels.Modules
{
    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleViewModel : IMapFrom<Module>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageData { get; set; }

        public bool IsActive { get; set; }
    }
}