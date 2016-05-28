namespace HappyMe.Web.Areas.Administration.ViewModels.Modules
{
    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleGridViewModel : IMapFrom<Module>, IMapTo<Module>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Module, ModuleGridViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(e => e.Author.UserName));
        }
    }
}