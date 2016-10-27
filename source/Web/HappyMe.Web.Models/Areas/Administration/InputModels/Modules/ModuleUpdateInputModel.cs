namespace HappyMe.Web.Models.Areas.Administration.InputModels.Modules
{
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

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