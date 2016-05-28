namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.Web.Mvc;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleUpdateInputModel : ModuleCreateInputModel, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ModuleUpdateInputModel, Module>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());
        }
    }
}